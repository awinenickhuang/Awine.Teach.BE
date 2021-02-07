using AutoMapper;
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
    /// 班级 -> 排课信息
    /// </summary>
    public class CourseScheduleService : ICourseScheduleService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<CourseScheduleService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// ICourseScheduleRepository
        /// </summary>
        private readonly ICourseScheduleRepository _courseScheduleRepository;

        /// <summary>
        /// ICourseRepository
        /// </summary>
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// IClassesRepository
        /// </summary>
        private readonly IClassesRepository _classesRepository;

        /// <summary>
        /// ILegalHolidayRepository
        /// </summary>
        private readonly ILegalHolidayRepository _legalHolidayRepository;

        /// <summary>
        /// IClassRoomRepository
        /// </summary>
        private readonly IClassRoomRepository _classRoomRepository;

        /// <summary>
        /// 构造 - > 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="courseScheduleRepository"></param>
        /// <param name="courseRepository"></param>
        /// <param name="classesRepository"></param>
        /// <param name="legalHolidayRepository"></param>
        /// <param name="classRoomRepository"></param>
        public CourseScheduleService(
            IMapper mapper,
            ILogger<CourseScheduleService> logger,
            ICurrentUser user,
            ICourseScheduleRepository courseScheduleRepository,
            ICourseRepository courseRepository,
            IClassesRepository classesRepository,
            ILegalHolidayRepository legalHolidayRepository,
            IClassRoomRepository classRoomRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _courseScheduleRepository = courseScheduleRepository;
            _courseRepository = courseRepository;
            _classesRepository = classesRepository;
            _legalHolidayRepository = legalHolidayRepository;
            _classRoomRepository = classRoomRepository;
        }

        /// <summary>
        /// 排课信息 -> 所有数据 -> 用于初始化课表日历 -> 按天统计（每天排了多少节课）
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="courseDates"></param>
        /// <param name="classStatus">课节状态 1-待上课 2-已结课</param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseScheduleStatisticalViewModel>> GetStatisticsDaily(string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", string courseDates = "", int classStatus = 0)
        {
            if (!string.IsNullOrEmpty(courseDates))
            {
                courseDates = Convert.ToDateTime(courseDates).ToString("yyyy-MM-dd HH:mm:ss");
            }

            var entities = await _courseScheduleRepository.GetAll(_user.TenantId, courseId, classId, teacherId, classRoomId, courseDates, 0);

            var classCourseScheduleCalendarViewModels = new List<CourseScheduleStatisticalViewModel>();

            var query =
            from schedule in entities
            group schedule by schedule.CourseDates into scheduleCountTemp
            select new
            {
                scheduleCountTemp.Key,
                scheduleCount = scheduleCountTemp.Count()
            };

            foreach (var item in query)
            {
                var model = new CourseScheduleStatisticalViewModel
                {
                    ScheduleDate = string.Format("{0:d}", item.Key),
                    ScheduleCount = item.scheduleCount
                };
                classCourseScheduleCalendarViewModels.Add(model);
            }

            return classCourseScheduleCalendarViewModels;
        }

        /// <summary>
        /// 排课信息 -> 所有数据 ->用于初始化课表日历 -> 携带日历组件需要的信息
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="courseDates"></param>
        /// <param name="classStatus">课节状态 1-待上课 2-已结课</param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseScheduleCalendarViewModel>> GetAll(string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", string courseDates = "", int classStatus = 0)
        {
            if (!string.IsNullOrEmpty(courseDates))
            {
                courseDates = Convert.ToDateTime(courseDates).ToString("yyyy-MM-dd HH:mm:ss");
            }

            var entities = await _courseScheduleRepository.GetAll(_user.TenantId, courseId, classId, teacherId, classRoomId, courseDates, 0);

            var classCourseScheduleCalendarViewModels = new List<CourseScheduleCalendarViewModel>();

            foreach (var item in entities)
            {
                string itemColor = "#99CC66";
                switch (item.ScheduleIdentification)
                {
                    case 1:
                        itemColor = "#99CC66";
                        break;
                    case 2:
                        itemColor = "#006699";
                        break;
                    case 3:
                        itemColor = "#FF6600";
                        break;
                }
                var model = new CourseScheduleCalendarViewModel
                {
                    Id = item.Id,
                    ScheduleId = item.Id,
                    Year = item.Year,
                    ClassId = item.ClassId,
                    ClassesName = item.ClassName,
                    Title = item.ClassName,
                    Start = item.CourseDates.ToString("yyyy-MM-dd") + " " + item.StartHours.ToString().PadLeft(2, '0') + ":" + item.StartMinutes.ToString().PadLeft(2, '0'),
                    End = item.CourseDates.ToString("yyyy-MM-dd") + " " + item.EndHours.ToString().PadLeft(2, '0') + ":" + item.EndMinutes.ToString().PadLeft(2, '0'),
                    Color = itemColor,
                    CourseDates = item.CourseDates,
                    StartHours = item.StartHours,
                    StartMinutes = item.StartMinutes,
                    EndHours = item.EndHours,
                    EndMinutes = item.EndMinutes,
                    TeacherId = item.TeacherId,
                    TeacherName = item.TeacherName,
                    CourseId = item.CourseId,
                    CourseName = item.CourseName,
                    ClassStatus = item.ClassStatus,
                    ScheduleIdentification = item.ScheduleIdentification,
                    ClassRoomId = item.ClassRoomId,
                    ClassRoom = item.ClassRoom,
                    ActualAttendanceNumber = item.ActualAttendanceNumber,
                    ActualleaveNumber = item.ActualleaveNumber,
                    ActualAbsenceNumber = item.ActualAbsenceNumber,
                    CreateTime = item.CreateTime
                };
                classCourseScheduleCalendarViewModels.Add(model);
            }

            return classCourseScheduleCalendarViewModels;
        }

        /// <summary>
        /// 排课信息 -> 所有数据 ->用于查询课程明细
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="courseDates"></param>
        /// <param name="classStatus"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseScheduleViewModel>> GetAllSchedule(string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", string courseDates = "", int classStatus = 0)
        {
            if (!string.IsNullOrEmpty(courseDates))
            {
                courseDates = Convert.ToDateTime(courseDates).ToString("yyyy-MM-dd HH:mm:ss");
            }

            var entities = await _courseScheduleRepository.GetAll(_user.TenantId, courseId, classId, teacherId, classRoomId, courseDates, classStatus);

            return _mapper.Map<IEnumerable<CourseSchedule>, IEnumerable<CourseScheduleViewModel>>(entities);
        }

        /// <summary>
        /// 排课信息 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="classStatus"></param>
        /// <param name="scheduleIdentification"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPagedList<CourseScheduleViewModel>> GetPageList(int page = 1, int limit = 15, string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", int classStatus = 0, int scheduleIdentification = 0, string beginDate = "", string endDate = "")
        {
            var entities = await _courseScheduleRepository.GetPageList(page, limit, _user.TenantId, courseId, classId, teacherId, classRoomId, classStatus, scheduleIdentification, beginDate, endDate);

            return _mapper.Map<IPagedList<CourseSchedule>, IPagedList<CourseScheduleViewModel>>(entities);
        }

        #region --------------------------------Private Methods--------------------------------

        /// <summary>
        /// 判断某一天是不是节假日
        /// </summary>
        /// <param name="legalHolidays"></param>
        /// <param name="currentDay"></param>
        /// <returns></returns>
        private bool IsAholidayDate(IEnumerable<LegalHoliday> legalHolidays, DateTime currentDay)
        {
            if (legalHolidays.Where(x => (string.Format("{0:d}", x.HolidayDate)).Equals(string.Format("{0:d}", currentDay))).Count() > 0)
            {
                return true;
            }
            return false;
        }


        #endregion

        /// <summary>
        /// 生成排课计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> AddClassSchedulingPlans(CourseScheduleAddViewModel model)
        {
            //排课开始时间 -> 第一天
            DateTime classOpeningDate = Convert.ToDateTime(model.ClassOpeningDate);

            if (classOpeningDate.Year != DateTime.Now.ToLocalTime().Year)
            {
                return new Result { Success = false, Message = "只能对当前年份进行排课！" };
            }

            var classes = await _classesRepository.GetModel(model.ClassId);
            if (null == classes)
            {
                return new Result { Success = false, Message = "未找到班级信息！" };
            }

            if (classes.RecruitStatus == 3)
            {
                return new Result { Success = false, Message = $"班级[{classes.Name}]已经结束授课了！" };
            }

            var holidays = await _legalHolidayRepository.GetAll(DateTime.Now.Year);

            var course = await _courseRepository.GetModel(model.CourseId);
            if (null == course)
            {
                return new Result { Success = false, Message = "未找到课程信息！" };
            }

            var classRoom = await _classRoomRepository.GetModel(model.ClassRoomId);
            if (null == classRoom)
            {
                return new Result { Success = false, Message = "未找到教室信息！" };
            }

            //从开始时到到结束时间共有哪些天可以用于排课 -> 决定了生成课表记录的条数
            var courseDates = new List<DateTime>();

            int schedulingDays = 0;
            IList<CourseSchedule> classCourseSchedules = new List<CourseSchedule>();

            switch (model.RepeatedWay)
            {
                case 1:
                    if (model.WeekDays.Count <= 0)
                    {
                        return new Result { Success = false, Message = "每周循环排课时，必须指定排课规则！" };
                    }
                    if (model.SescheduleEndWay.SescheduleEndWayNumber == 1)
                    {
                        //如果结束方式按总课节数，只需要找够满足排课规则的哪些天即可
                        schedulingDays = model.SescheduleEndWay.SescheduleEndWaySessionsNumber;
                        if (model.ExclusionRule)
                        {
                            if (!IsAholidayDate(holidays, classOpeningDate) & model.WeekDays.Where(w => w == TimeCalculate.DayOfWeek(classOpeningDate)).Count() > 0)
                            {
                                courseDates.Add(classOpeningDate);
                            }
                        }
                        else
                        {
                            if (model.WeekDays.Where(w => w == TimeCalculate.DayOfWeek(classOpeningDate)).Count() > 0)
                            {
                                courseDates.Add(classOpeningDate);
                            }
                        }
                        do
                        {
                            if (model.ExclusionRule)
                            {
                                //下一天
                                var currentDay = classOpeningDate.AddDays(1);
                                if (!IsAholidayDate(holidays, currentDay) & model.WeekDays.Where(w => w == TimeCalculate.DayOfWeek(currentDay)).Count() > 0)
                                {
                                    courseDates.Add(currentDay);
                                }
                                classOpeningDate = currentDay;
                            }
                            else
                            {
                                //下一天
                                var currentDay = classOpeningDate.AddDays(1);
                                if (model.WeekDays.Where(w => w == TimeCalculate.DayOfWeek(currentDay)).Count() > 0)
                                {
                                    courseDates.Add(currentDay);
                                }
                                classOpeningDate = currentDay;
                            }
                        } while (courseDates.Count() < schedulingDays);
                    }
                    if (model.SescheduleEndWay.SescheduleEndWayNumber == 2)
                    {
                        var endTime = Convert.ToDateTime(model.SescheduleEndWay.SescheduleEndWayClassEndDate);
                        if (classOpeningDate >= endTime)
                        {
                            return new Result { Success = false, Message = "每周循环排课时，必须指定排课规则！" };
                        }
                        //如果按结束日期，只需找到开始日期与结束日期之间满足排课规则的哪些天即可

                        schedulingDays = TimeCalculate.DateDiff(classOpeningDate, Convert.ToDateTime(model.SescheduleEndWay.SescheduleEndWayClassEndDate));
                        if (schedulingDays < 1)
                        {
                            return new Result { Success = false, Message = "开课时间与结束时间至少相隔一天！" };
                        }

                        //如果需要排除掉法定假日
                        if (model.ExclusionRule)
                        {
                            //第一天
                            if (!IsAholidayDate(holidays, classOpeningDate) & model.WeekDays.Where(w => w == TimeCalculate.DayOfWeek(classOpeningDate)).Count() > 0)
                            {
                                courseDates.Add(classOpeningDate);
                            }
                            //第一天后的每一天
                            for (int i = 0; i < schedulingDays; i++)
                            {
                                var currentDay = classOpeningDate.AddDays(i + 1);
                                if (!IsAholidayDate(holidays, currentDay) & model.WeekDays.Where(w => w == TimeCalculate.DayOfWeek(currentDay)).Count() > 0)
                                {
                                    courseDates.Add(currentDay);
                                }
                            }
                        }
                        else//不需要排除掉法定假日
                        {
                            //第一天
                            if (model.WeekDays.Where(w => w == TimeCalculate.DayOfWeek(classOpeningDate)).Count() > 0)
                            {
                                courseDates.Add(classOpeningDate);
                            }
                            //第一天后的每一天
                            for (int i = 0; i < schedulingDays; i++)
                            {
                                var currentDay = classOpeningDate.AddDays(i + 1);
                                if (model.WeekDays.Where(w => w == TimeCalculate.DayOfWeek(currentDay)).Count() > 0)
                                {
                                    courseDates.Add(currentDay);
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    //每天排课
                    if (model.ExclusionRule)
                    {
                        if (!IsAholidayDate(holidays, classOpeningDate))
                        {
                            courseDates.Add(classOpeningDate);
                        }
                    }
                    else
                    {
                        courseDates.Add(classOpeningDate);
                    }

                    if (model.SescheduleEndWay.SescheduleEndWayNumber == 1)//按总课节数结束
                    {
                        do
                        {
                            //之后的每一天
                            var currentDay = classOpeningDate.AddDays(1);
                            if (model.ExclusionRule)
                            {
                                if (!IsAholidayDate(holidays, currentDay))
                                {
                                    courseDates.Add(currentDay);
                                }
                                classOpeningDate = currentDay;
                            }
                            else
                            {
                                courseDates.Add(currentDay);
                                classOpeningDate = currentDay;
                            }

                        } while (courseDates.Count() < model.SescheduleEndWay.SescheduleEndWaySessionsNumber);
                    }
                    if (model.SescheduleEndWay.SescheduleEndWayNumber == 2)//按结束日期结束
                    {
                        schedulingDays = TimeCalculate.DateDiff(classOpeningDate, Convert.ToDateTime(model.SescheduleEndWay.SescheduleEndWayClassEndDate));
                        if (schedulingDays < 1)
                        {
                            return new Result { Success = false, Message = "开课时间与结束时间至少相隔一天！" };
                        }

                        do
                        {
                            //间隔天
                            var currentDay = classOpeningDate.AddDays(1);

                            if (model.ExclusionRule)
                            {
                                if (!IsAholidayDate(holidays, currentDay) & currentDay <= Convert.ToDateTime(model.SescheduleEndWay.SescheduleEndWayClassEndDate))
                                {
                                    courseDates.Add(currentDay);
                                }
                                classOpeningDate = currentDay;
                            }
                            else
                            {
                                if (currentDay <= Convert.ToDateTime(model.SescheduleEndWay.SescheduleEndWayClassEndDate))
                                {
                                    courseDates.Add(currentDay);
                                }
                                classOpeningDate = currentDay;
                            }

                        } while (classOpeningDate <= Convert.ToDateTime(model.SescheduleEndWay.SescheduleEndWayClassEndDate));
                    }
                    break;
                case 3:
                    //只排一次
                    courseDates.Add(classOpeningDate);
                    break;
                case 4:
                    //第一天
                    if (model.ExclusionRule)
                    {
                        if (!IsAholidayDate(holidays, classOpeningDate))
                        {
                            courseDates.Add(classOpeningDate);
                        }
                    }
                    else
                    {
                        courseDates.Add(classOpeningDate);
                    }

                    if (model.SescheduleEndWay.SescheduleEndWayNumber == 1)//按结束课节数
                    {
                        do
                        {
                            //间隔天
                            var currentDay = classOpeningDate.AddDays(model.DaysBetween + 1);

                            if (model.ExclusionRule)
                            {
                                if (!IsAholidayDate(holidays, currentDay))
                                {
                                    courseDates.Add(currentDay);
                                }
                                classOpeningDate = currentDay;
                            }
                            else
                            {
                                courseDates.Add(currentDay);
                                classOpeningDate = currentDay;
                            }

                        } while (courseDates.Count() < model.SescheduleEndWay.SescheduleEndWaySessionsNumber);
                    }
                    if (model.SescheduleEndWay.SescheduleEndWayNumber == 2)//按结束日期
                    {
                        schedulingDays = TimeCalculate.DateDiff(classOpeningDate, Convert.ToDateTime(model.SescheduleEndWay.SescheduleEndWayClassEndDate));
                        if (schedulingDays < 1)
                        {
                            return new Result { Success = false, Message = "开课时间与结束时间至少相隔一天！" };
                        }

                        do
                        {
                            //间隔天
                            var currentDay = classOpeningDate.AddDays(model.DaysBetween + 1);

                            if (model.ExclusionRule)
                            {
                                if (!IsAholidayDate(holidays, currentDay) & currentDay <= Convert.ToDateTime(model.SescheduleEndWay.SescheduleEndWayClassEndDate))
                                {
                                    courseDates.Add(currentDay);
                                }
                                classOpeningDate = currentDay;
                            }
                            else
                            {
                                if (currentDay <= Convert.ToDateTime(model.SescheduleEndWay.SescheduleEndWayClassEndDate))
                                {
                                    courseDates.Add(currentDay);
                                }
                                classOpeningDate = currentDay;
                            }

                        } while (classOpeningDate <= Convert.ToDateTime(model.SescheduleEndWay.SescheduleEndWayClassEndDate));
                    }
                    break;
            }

            //从开始时到到结束时间共有哪些天可以用于排课 -> 按需排除节假日
            foreach (var courseDate in courseDates)
            {
                //每天排几节课
                var schedule = new CourseSchedule()
                {
                    Year = DateTime.Now.ToLocalTime().Year,
                    ClassId = model.ClassId,
                    ClassName = classes.Name,
                    CourseDates = courseDate,
                    StartHours = model.ClassTime.StartHours,
                    StartMinutes = model.ClassTime.StartMinutes,
                    EndHours = model.ClassTime.EndHours,
                    EndMinutes = model.ClassTime.EndMinutes,
                    TeacherId = model.TeacherId,
                    TeacherName = model.TeacherName,
                    CourseId = model.CourseId,
                    CourseName = course.Name,
                    ClassStatus = 1,
                    ScheduleIdentification = 1,
                    ClassRoomId = model.ClassRoomId,
                    ClassRoom = classRoom.Name,
                    ActualAttendanceNumber = 0,
                    ActualleaveNumber = 0,
                    ActualAbsenceNumber = 0,
                    TenantId = _user.TenantId
                };
                classCourseSchedules.Add(schedule);
            }

            if (classCourseSchedules.Count < 1)
            {
                return new Result { Success = false, Message = "当前排课条件，找不到合适的排课日期！" };
            }

            if (await _courseScheduleRepository.AddClassSchedulingPlans(classCourseSchedules) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(CourseScheduleUpdateViewModel model)
        {
            var schedule = await _courseScheduleRepository.GetModel(model.Id);
            if (null == schedule)
            {
                return new Result { Success = false, Message = "未找到课节信息！" };
            }

            if (schedule.ClassStatus == 2)
            {
                return new Result { Success = false, Message = "该课节已结课，不允许进行调整操作！" };
            }

            var entity = _mapper.Map<CourseScheduleUpdateViewModel, CourseSchedule>(model);
            var classes = await _classesRepository.GetModel(model.ClassId);
            if (null == classes)
            {
                return new Result { Success = false, Message = "未找到班级信息！" };
            }
            entity.ClassName = classes.Name;
            var course = await _courseRepository.GetModel(model.CourseId);
            if (null == course)
            {
                return new Result { Success = false, Message = "未找到课程信息！" };
            }
            entity.CourseName = course.Name;
            var classRoom = await _classRoomRepository.GetModel(model.ClassRoomId);
            if (null == classRoom)
            {
                return new Result { Success = false, Message = "未找到教室信息！" };
            }
            entity.ClassRoom = classRoom.Name;

            var result = await _courseScheduleRepository.Update(entity);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Delete(string id)
        {
            var schedule = await _courseScheduleRepository.GetModel(id);
            if (null == schedule)
            {
                return new Result { Success = false, Message = "未找到课节信息！" };
            }

            if (schedule.ClassStatus == 2)
            {
                return new Result { Success = false, Message = "该课节已结课，不允许删除！" };
            }

            var result = await _courseScheduleRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
