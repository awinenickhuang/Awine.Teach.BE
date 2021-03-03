using AutoMapper;
using Awine.Framework.AspNetCore.Model;
using Awine.Framework.Core;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ServiceResult;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Awine.Teach.TeachingAffairService.Domain;
using Awine.Teach.TeachingAffairService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Services
{
    /// <summary>
    /// 学生考勤
    /// </summary>
    public class StudentAttendanceService : IStudentAttendanceService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ClassesService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 学生出勤信息
        /// </summary>
        private readonly IStudentAttendanceRepository _studentAttendanceRepository;

        /// <summary>
        /// 班级信息
        /// </summary>
        private readonly IClassesRepository _classesRepository;

        /// <summary>
        /// 教室信息
        /// </summary>
        private readonly IClassRoomRepository _classRoomRepository;

        /// <summary>
        /// 课程信息
        /// </summary>
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// 学生信息
        /// </summary>
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// 课节信息
        /// </summary>
        private readonly ICourseScheduleRepository _classCourseScheduleRepository;

        /// <summary>
        /// 课程定价标准
        /// </summary>
        private readonly ICourseChargeMannerRepository _courseChargeMannerRepository;

        /// <summary>
        /// 学生报读课程
        /// </summary>
        private readonly IStudentCourseItemRepository _studentCourseItemRepository;

        /// <summary>
        /// 构造 -> 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="studentAttendanceRepository"></param>
        /// <param name="classesRepository"></param>
        /// <param name="classRoomRepository"></param>
        /// <param name="courseRepository"></param>
        /// <param name="studentRepository"></param>
        /// <param name="classCourseScheduleRepository"></param>
        /// <param name="courseChargeMannerRepository"></param>
        /// <param name="studentCourseItemRepository"></param>
        public StudentAttendanceService(
            IMapper mapper,
            ILogger<ClassesService> logger,
            ICurrentUser user,
            IStudentAttendanceRepository studentAttendanceRepository,
            IClassesRepository classesRepository,
            IClassRoomRepository classRoomRepository,
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,
            ICourseScheduleRepository classCourseScheduleRepository,
            ICourseChargeMannerRepository courseChargeMannerRepository,
            IStudentCourseItemRepository studentCourseItemRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _studentAttendanceRepository = studentAttendanceRepository;
            _classesRepository = classesRepository;
            _classRoomRepository = classRoomRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _classCourseScheduleRepository = classCourseScheduleRepository;
            _courseChargeMannerRepository = courseChargeMannerRepository;
            _studentCourseItemRepository = studentCourseItemRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="classId"></param>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <param name="studentName"></param>
        /// <param name="attendanceStatus"></param>
        /// <param name="scheduleIdentification"></param>
        /// <param name="processingStatus"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPagedList<StudentAttendanceViewModel>> GetPageList(int page = 1, int limit = 15, string classId = "", string courseId = "", string studentId = "", string studentName = "", int attendanceStatus = 0, int scheduleIdentification = 0, int processingStatus = 0, string beginDate = "", string endDate = "")
        {
            var entities = await _studentAttendanceRepository.GetPageList(page, limit, _user.TenantId, classId, courseId, studentId, studentName, attendanceStatus, scheduleIdentification, processingStatus, beginDate, endDate);

            return _mapper.Map<IPagedList<StudentAttendance>, IPagedList<StudentAttendanceViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StudentAttendanceViewModel> GetModel(string id)
        {
            var entity = await _studentAttendanceRepository.GetModel(id);
            return _mapper.Map<StudentAttendance, StudentAttendanceViewModel>(entity);
        }

        /// <summary>
        /// 课节点名 -> 出勤情况 -> 正式课节 -> 包括在读学生#跟班试听学生
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Attendance(SignInViewModel model)
        {
            var existingSchedule = await _classCourseScheduleRepository.GetModel(model.CourseScheduleId);
            if (null == existingSchedule)
            {
                return new Result { Success = false, Message = "未找到课节信息！" };
            }
            if (existingSchedule.ClassStatus == 2)
            {
                return new Result { Success = false, Message = "课节已结课，不允许再点名了哦！" };
            }
            //课节信息
            CourseSchedule schedule = new CourseSchedule()
            {
                Id = model.CourseScheduleId,
                ClassStatus = 2
            };
            //统计 - 课节信息 - 出勤情况
            schedule.ActualAttendanceNumber = model.ListenStudents.Where(x => x.ListeningState == 2).Count() + model.OfficialStudents.Where(x => x.AttendanceState == 1).Count();
            schedule.ActualAbsenceNumber = model.ListenStudents.Where(x => x.ListeningState == 3).Count() + model.OfficialStudents.Where(x => x.AttendanceState == 2).Count();
            schedule.ActualleaveNumber = model.OfficialStudents.Where(x => x.AttendanceState == 3).Count();

            //学生报读信息 - > 课消课程
            IList<StudentCourseItem> studentCourseItems = new List<StudentCourseItem>();

            //正式学生出勤信息
            IList<StudentAttendance> studentAttendances = new List<StudentAttendance>();

            foreach (var item in model.OfficialStudents)
            {
                var existingStudentCourseItem = await _studentCourseItemRepository.GetModel(item.StudentCourseItemId);
                if (null == existingStudentCourseItem)
                {
                    return new Result { Success = false, Message = "未找报课消课程信息！" };
                }

                //每节课每学生的课消数量
                int studentConsumedQuantity = 0;

                //每节课每学生的课消金额
                decimal studentConsumptionAmount = 0.00M;

                //按课时收费时
                if (existingStudentCourseItem.ChargeManner == 1)
                {
                    //每个学生的课消数量 -> 前端传来的值
                    studentConsumedQuantity = item.ConsumedQuantity;
                    //每个学生的课消金额 -> 报名时采用的定价标准的课时的单价 乘以 课消数量
                    studentConsumptionAmount += item.ConsumedQuantity * existingStudentCourseItem.UnitPrice;
                }

                //按月收费时
                if (existingStudentCourseItem.ChargeManner == 2)
                {
                    //DO Nothing
                }

                studentCourseItems.Add(new StudentCourseItem()
                {
                    Id = item.StudentCourseItemId,
                    ConsumedQuantity = existingStudentCourseItem.ConsumedQuantity + item.ConsumedQuantity,
                    RemainingNumber = existingStudentCourseItem.RemainingNumber - studentConsumedQuantity
                });

                var processingStatus = 1;

                if ((item.AttendanceState == 2 || item.AttendanceState == 3) && existingSchedule.ScheduleIdentification == 1)
                {
                    processingStatus = 2;
                }

                studentAttendances.Add(new StudentAttendance()
                {
                    StudentId = item.StudentId,
                    StudentName = item.StudentName,
                    ClassId = existingSchedule.ClassId,
                    ClassName = existingSchedule.ClassName,
                    CourseId = existingSchedule.CourseId,
                    CourseName = existingSchedule.CourseName,
                    StudentCourseItemId = existingStudentCourseItem.Id,
                    ClassCourseScheduleId = model.CourseScheduleId,
                    ScheduleIdentification = existingSchedule.ScheduleIdentification,
                    AttendanceStatus = item.AttendanceState,
                    RecordStatus = 1,
                    ConsumedQuantity = studentConsumedQuantity,
                    ProcessingStatus = processingStatus,
                    TenantId = _user.TenantId,
                    IsDeleted = false
                });

                //课节 -> 课消总数量
                schedule.ConsumedQuantity += studentConsumedQuantity;
                //课节 -> 课消总金额
                schedule.ConsumedAmount += studentConsumptionAmount;
            }

            //试听学生到课信息
            IList<TrialClass> trialClasses = new List<TrialClass>();

            foreach (var m in model.ListenStudents)
            {
                trialClasses.Add(new TrialClass()
                {
                    Id = m.Id,
                    ListeningState = m.ListeningState
                });
            }

            if (await _studentAttendanceRepository.Attendance(schedule, studentCourseItems, studentAttendances, trialClasses))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 课节点名 -> 出勤情况 -> 一对一试听课节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> TrialClassSigninAttendance(SignInViewModel model)
        {
            var existingSchedule = await _classCourseScheduleRepository.GetModel(model.CourseScheduleId);
            if (null == existingSchedule)
            {
                return new Result { Success = false, Message = "未找到课节信息！" };
            }
            if (existingSchedule.ClassStatus == 2)
            {
                return new Result { Success = false, Message = "课节已结课，不允许再点名了哦！" };
            }
            //课节信息
            CourseSchedule schedule = new CourseSchedule()
            {
                Id = model.CourseScheduleId,
                ClassStatus = 2
            };
            //统计 - 课节信息 - 出勤情况
            schedule.ActualAttendanceNumber = model.ListenStudents.Where(x => x.ListeningState == 2).Count();
            schedule.ActualAbsenceNumber = model.ListenStudents.Where(x => x.ListeningState == 3).Count();
            //试听课无请假状态
            schedule.ActualleaveNumber = 0;
            //课节 -> 课消总数量 -> 试听课为0
            schedule.ConsumedQuantity = 0;
            //课节 -> 课消总金额 -> 试听课为0
            schedule.ConsumedAmount = 0.00M;

            //试听学生到课信息
            IList<TrialClass> trialClasses = new List<TrialClass>();

            foreach (var m in model.ListenStudents)
            {
                trialClasses.Add(new TrialClass()
                {
                    Id = m.Id,
                    ListeningState = m.ListeningState
                });
            }

            if (await _studentAttendanceRepository.TrialClassSigninAttendance(schedule, trialClasses))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 课节点名 -> 出勤情况 -> 补课课节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> MakeupMissedLessonAttendance(MakeupMissedLessonAttendanceViewModel model)
        {
            var existingSchedule = await _classCourseScheduleRepository.GetModel(model.CourseScheduleId);

            if (null == existingSchedule)
            {
                return new Result { Success = false, Message = "未找到课节信息！" };
            }

            if (existingSchedule.ClassStatus == 2)
            {
                return new Result { Success = false, Message = "课节已结课，不允许再点名了哦！" };
            }

            //课节信息
            CourseSchedule schedule = new CourseSchedule()
            {
                Id = model.CourseScheduleId,
                ClassStatus = 2
            };

            //统计 - 课节信息 - 出勤情况
            schedule.ActualAttendanceNumber = model.MakeupMissedLessonStudents.Where(x => x.AttendanceState == 1).Count();
            schedule.ActualAbsenceNumber = model.MakeupMissedLessonStudents.Where(x => x.AttendanceState == 2).Count();
            schedule.ActualleaveNumber = model.MakeupMissedLessonStudents.Where(x => x.AttendanceState == 3).Count();

            //学生报读信息 - > 课消课程
            IList<StudentCourseItem> studentCourseItems = new List<StudentCourseItem>();

            //补课学生出勤信息
            IList<StudentAttendance> studentAttendances = new List<StudentAttendance>();

            //补课学生需要课课的勤信息 -> 当前是补哪些需要补课的出勤记录
            IList<StudentAttendance> makeupMissedLessonStudentAttendances = new List<StudentAttendance>();

            foreach (var item in model.MakeupMissedLessonStudents)
            {
                var existingStudentCourseItem = await _studentCourseItemRepository.GetModel(item.StudentCourseItemId);
                if (null == existingStudentCourseItem)
                {
                    return new Result { Success = false, Message = "未找报课消课程信息！" };
                }

                //每节课每学生的课消数量
                int studentConsumedQuantity = 0;

                //每节课每学生的课消金额
                decimal studentConsumptionAmount = 0.00M;

                //按课时收费时
                if (existingStudentCourseItem.ChargeManner == 1)
                {
                    //每个学生的课消数量 -> 前端传来的值
                    studentConsumedQuantity = item.ConsumedQuantity;
                    //每个学生的课消金额 -> 报名时采用的定价标准的课时的单价 乘以 课消数量
                    studentConsumptionAmount += item.ConsumedQuantity * existingStudentCourseItem.UnitPrice;
                }

                //按月收费时
                if (existingStudentCourseItem.ChargeManner == 2)
                {
                    //DO Nothing
                }

                studentCourseItems.Add(new StudentCourseItem()
                {
                    Id = item.StudentCourseItemId,
                    ConsumedQuantity = existingStudentCourseItem.ConsumedQuantity + item.ConsumedQuantity,
                    RemainingNumber = existingStudentCourseItem.RemainingNumber - studentConsumedQuantity
                });

                var processingStatus = 1;

                if ((item.AttendanceState == 2 || item.AttendanceState == 3) && existingSchedule.ScheduleIdentification == 1)
                {
                    processingStatus = 2;
                }

                studentAttendances.Add(new StudentAttendance()
                {
                    StudentId = item.StudentId,
                    StudentName = item.StudentName,
                    ClassId = existingSchedule.ClassId,
                    ClassName = existingSchedule.ClassName,
                    CourseId = existingSchedule.CourseId,
                    CourseName = existingSchedule.CourseName,
                    StudentCourseItemId = existingStudentCourseItem.Id,
                    ClassCourseScheduleId = model.CourseScheduleId,
                    ScheduleIdentification = existingSchedule.ScheduleIdentification,
                    AttendanceStatus = item.AttendanceState,
                    RecordStatus = 1,
                    ConsumedQuantity = studentConsumedQuantity,
                    ProcessingStatus = processingStatus,
                    TenantId = _user.TenantId,
                    IsDeleted = false
                });

                //课节 -> 课消总数量
                schedule.ConsumedQuantity += studentConsumedQuantity;
                //课节 -> 课消总金额
                schedule.ConsumedAmount += studentConsumptionAmount;

                int currentPprocessingStatus = 2;

                if (item.AttendanceState == 1)
                {
                    currentPprocessingStatus = 4;
                }

                makeupMissedLessonStudentAttendances.Add(new StudentAttendance()
                {
                    Id = item.AttendanceId,
                    ProcessingStatus = currentPprocessingStatus
                });
            }

            //补课班级信息
            MakeupMissedLesson makeupMissedLesson = new MakeupMissedLesson()
            {
                Id = model.MakeupMissedLessonId,
                Status = 2
            };

            if (await _studentAttendanceRepository.MakeupMissedLessonAttendance(schedule, studentCourseItems, studentAttendances, makeupMissedLessonStudentAttendances, makeupMissedLesson))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 取消考勤
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> CancelAttendance(string id)
        {
            /*
             * TO DO
             * 1-是否退回已扣课时 t_ea_student_course_item
             * 2-更新考勤记录为 RecordStatus 2 取消
             * 3-是否有其他影响？
             */

            var attendance = await _studentAttendanceRepository.GetModel(id);

            if (null == attendance)
            {
                return new Result { Success = false, Message = "未找到考勤记录！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        #region 统计分析

        /// <summary>
        /// 课消金额统计分析
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<BasicBarChartViewModel> AttendanceAmountReport(string date)
        {
            BasicBarChartViewModel chartData = new BasicBarChartViewModel();
            var attendances = await _studentAttendanceRepository.GetAll(tenantId: _user.TenantId);

            DateTime time = Convert.ToDateTime(date);

            var dateFrom = TimeCalculate.FirstDayOfMonth(time);
            attendances = attendances.Where(x => x.CreateTime >= dateFrom);

            var dateTo = TimeCalculate.LastDayOfMonth(time);
            attendances = attendances.Where(x => x.CreateTime <= dateTo);

            //当前月天数
            int days = TimeCalculate.DaysInMonth(time);

            for (int day = 1; day <= days; day++)
            {
                chartData.XAxis.Add($"{time.Year}-{time.Month}-{day}");
                var dayAttendances = attendances.Where(x => x.CreateTime.Year == time.Year && x.CreateTime.Month == time.Month && x.CreateTime.Day == day);
                var amount = 0M;
                foreach (var da in dayAttendances)
                {
                    amount += da.ConsumedQuantity;
                }
                //填充 图表 数据
                chartData.SeriesDecimalData.Add(attendances.Where(x => x.CreateTime.Year == time.Year && x.CreateTime.Month == time.Month && x.CreateTime.Day == day).Count());
            }

            return chartData;
        }

        /// <summary>
        /// 课消数量统计分析
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<BasicBarChartViewModel> AttendanceNumberReport(string date)
        {
            BasicBarChartViewModel chartData = new BasicBarChartViewModel();
            var attendances = await _studentAttendanceRepository.GetAll(tenantId: _user.TenantId);

            DateTime time = Convert.ToDateTime(date);

            var dateFrom = TimeCalculate.FirstDayOfMonth(time);
            attendances = attendances.Where(x => x.CreateTime >= dateFrom);

            var dateTo = TimeCalculate.LastDayOfMonth(time);
            attendances = attendances.Where(x => x.CreateTime <= dateTo);

            //当前月天数
            int days = TimeCalculate.DaysInMonth(time);

            for (int day = 1; day <= days; day++)
            {
                chartData.XAxis.Add($"{time.Year}-{time.Month}-{day}");
                var dayAttendances = attendances.Where(x => x.CreateTime.Year == time.Year && x.CreateTime.Month == time.Month && x.CreateTime.Day == day);
                var amount = 0L;
                foreach (var da in dayAttendances)
                {
                    amount += da.ConsumedQuantity;
                }
                //填充 图表 数据
                chartData.SeriesLongData.Add(amount);
            }

            return chartData;
        }

        #endregion
    }
}