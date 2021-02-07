using AutoMapper;
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
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Services
{
    /// <summary>
    /// 试听管理
    /// </summary>
    public class TrialClassService : ITrialClassService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<TrialClassService> _logger;

        /// <summary>
        /// IUser
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 试听记录
        /// </summary>
        private readonly ITrialClassRepository _trialClassRepository;

        /// <summary>
        /// 咨询记录
        /// </summary>
        private readonly IConsultRecordRepository _consultRecordRepository;

        /// <summary>
        /// 课程信息
        /// </summary>
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// 班级信息
        /// </summary>
        private readonly IClassesRepository _classesRepository;

        /// <summary>
        /// 教室信息
        /// </summary>
        private readonly IClassRoomRepository _classRoomRepository;

        /// <summary>
        /// 学生信息
        /// </summary>
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// 课表信息
        /// </summary>
        private readonly ICourseScheduleRepository _courseScheduleRepository;

        /// <summary>
        /// 构造函数 - 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="trialClassRepository"></param>
        /// <param name="consultRecordRepository"></param>
        /// <param name="courseRepository"></param>
        /// <param name="classesRepository"></param>
        /// <param name="classRoomRepository"></param>
        /// <param name="studentRepository"></param>
        /// <param name="courseScheduleRepository"></param>
        public TrialClassService(
            IMapper mapper,
            ILogger<TrialClassService> logger,
            ICurrentUser user,
            ITrialClassRepository trialClassRepository,
            IConsultRecordRepository consultRecordRepository,
            ICourseRepository courseRepository,
            IClassesRepository classesRepository,
            IClassRoomRepository classRoomRepository,
            IStudentRepository studentRepository,
            ICourseScheduleRepository courseScheduleRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _trialClassRepository = trialClassRepository;
            _consultRecordRepository = consultRecordRepository;
            _courseRepository = courseRepository;
            _classesRepository = classesRepository;
            _classRoomRepository = classRoomRepository;
            _studentRepository = studentRepository;
            _courseScheduleRepository = courseScheduleRepository;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="listeningState"></param>
        /// <param name="courseScheduleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TrialClassViewModel>> GetAll(int listeningState = 0, string courseScheduleId = "")
        {
            var entities = await _trialClassRepository.GetAll(_user.TenantId, listeningState, courseScheduleId);
            return _mapper.Map<IEnumerable<TrialClass>, IEnumerable<TrialClassViewModel>>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="listeningState"></param>
        /// <param name="teacherId"></param>
        /// <param name="courseId"></param>
        /// <param name="courseScheduleId"></param>
        /// <returns></returns>
        public async Task<IPagedList<TrialClassViewModel>> GetPageList(int page = 1, int limit = 15, string name = "", string phoneNumber = "", int listeningState = 0, string teacherId = "", string courseId = "", string courseScheduleId = "")
        {
            var entities = await _trialClassRepository.GetPageList(page, limit, _user.TenantId, name, phoneNumber, listeningState, teacherId, courseId, courseScheduleId);
            return _mapper.Map<IPagedList<TrialClass>, IPagedList<TrialClassViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TrialClassViewModel> GetModel(string id)
        {
            var entity = await _trialClassRepository.GetModel(id);

            return _mapper.Map<TrialClass, TrialClassViewModel>(entity);
        }

        /// <summary>
        /// 跟班试听
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> ListenFollowClass(TrialClassListenViewModel model)
        {
            TrialClass trialClass = new TrialClass()
            {
                StudentId = model.StudentId,
                TrialClassGenre = 1,
                CreatorId = _user.UserId,
                CreatorName = _user.Name,
                TeacherId = _user.TenantId,
                CourseScheduleId = model.CourseScheduleId,
                TenantId = _user.TenantId,
                IsTransformation = false,
                ListeningState = 1,
                IsDeleted = false
            };
            var existing = await _trialClassRepository.GetModel(trialClass);
            if (null != existing)
            {
                return new Result { Success = false, Message = "已创建过本节课的试听信息！" };
            }
            if (model.StudentCategory == 1)
            {
                var record = await _consultRecordRepository.GetModel(model.StudentId);
                if (null == record)
                {
                    return new Result { Success = false, Message = "找不到学生信息！" };
                }
                trialClass.StudentName = record.Name;
                trialClass.Gender = record.Gender;
                trialClass.PhoneNumber = record.PhoneNumber;
                trialClass.StudentCategory = 1;
            }

            if (model.StudentCategory == 2)
            {
                var student = await _studentRepository.GetModel(model.StudentId);
                if (null == student)
                {
                    return new Result { Success = false, Message = "找不到学生信息！" };
                }
                trialClass.StudentName = student.Name;
                trialClass.Gender = student.Gender;
                trialClass.PhoneNumber = student.PhoneNumber;
                trialClass.StudentCategory = 2;
            }

            var schedule = await _courseScheduleRepository.GetModel(model.CourseScheduleId);
            if (null == schedule)
            {
                return new Result { Success = false, Message = "找不到课节信息！" };
            }
            trialClass.TeacherName = schedule.TeacherName;
            trialClass.CourseId = schedule.CourseId;
            trialClass.CourseName = schedule.CourseName;
            trialClass.CourseScheduleInformation = $"上课教室：{schedule.ClassRoom} 课节时间：{Convert.ToDateTime(schedule.CourseDates).ToString("yyyy-MM-dd")} {schedule.StartHours.ToString().PadLeft(2, '0')}:{schedule.StartMinutes.ToString().PadLeft(2, '0')} - {schedule.EndHours.ToString().PadLeft(2, '0')}:{schedule.EndMinutes.ToString().PadLeft(2, '0')}";
            if (await _trialClassRepository.ListenFollowClass(trialClass) > 0)
            {
                return new Result { Success = true, Message = "成功创建试听！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 一对一试听
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> OneToOneListen(TrialClassListenCourseViewModel model)
        {
            TrialClass trialClass = new TrialClass()
            {
                StudentId = model.StudentId,
                TrialClassGenre = 2,
                CreatorId = _user.UserId,
                CreatorName = _user.Name,
                TenantId = _user.TenantId,
                IsDeleted = false
            };

            if (model.StudentCategory == 1)
            {
                var record = await _consultRecordRepository.GetModel(model.StudentId);
                if (null == record)
                {
                    return new Result { Success = false, Message = "找不到学生信息！" };
                }
                trialClass.StudentName = record.Name;
                trialClass.Gender = record.Gender;
                trialClass.PhoneNumber = record.PhoneNumber;
                trialClass.StudentCategory = 1;
            }

            if (model.StudentCategory == 2)
            {
                var student = await _studentRepository.GetModel(model.StudentId);
                if (null == student)
                {
                    return new Result { Success = false, Message = "找不到学生信息！" };
                }
                trialClass.StudentName = student.Name;
                trialClass.Gender = student.Gender;
                trialClass.PhoneNumber = student.PhoneNumber;
                trialClass.StudentCategory = 2;
            }

            CourseSchedule courseSchedule = new CourseSchedule()
            {
                Year = DateTime.Now.ToLocalTime().Year,
                CourseId = model.CourseId,
                TeacherId = model.TeacherId,
                ClassId = Guid.Empty.ToString(),//一对一试听课时，课节数据不对应到班级
                ClassName = "试听课（一对一）",
                ClassRoomId = model.ClassRoomId,
                CourseDates = model.CourseDates,
                StartHours = model.StartHours,
                StartMinutes = model.StartMinutes,
                EndHours = model.EndHours,
                EndMinutes = model.EndMinutes,
                ClassStatus = 1,
                ScheduleIdentification = 2,
                ActualAttendanceNumber = 0,
                ActualleaveNumber = 0,
                ActualAbsenceNumber = 0,
                ConsumedQuantity = 0,
                ConsumedAmount = 0,
                TenantId = _user.TenantId,
                IsDeleted = false
            };

            trialClass.CourseScheduleId = courseSchedule.Id;

            //验证课程信息
            var course = await _courseRepository.GetModel(model.CourseId);
            if (null == course)
            {
                return new Result { Success = false, Message = "找不到课程信息！" };
            }
            trialClass.CourseId = model.CourseId;
            trialClass.CourseName = course.Name;
            courseSchedule.CourseName = course.Name;

            //验证教室信息
            var classesRoom = await _classRoomRepository.GetModel(model.ClassRoomId);
            if (null == classesRoom)
            {
                return new Result { Success = false, Message = "找不到教室信息！" };
            }
            courseSchedule.ClassRoom = classesRoom.Name;

            trialClass.TeacherId = model.TeacherId;
            trialClass.TeacherName = model.TeacherName;
            courseSchedule.TeacherName = model.TeacherName;

            trialClass.CourseScheduleInformation = $"上课教室：{courseSchedule.ClassRoom} 课节时间：{Convert.ToDateTime(courseSchedule.CourseDates).ToString("yyyy-MM-dd")} {courseSchedule.StartHours.ToString().PadLeft(2, '0')}:{courseSchedule.StartMinutes.ToString().PadLeft(2, '0')} - {courseSchedule.EndHours.ToString().PadLeft(2, '0')}:{courseSchedule.EndMinutes.ToString().PadLeft(2, '0')}";

            //提交试听课数据
            if (await _trialClassRepository.OneToOneListen(trialClass, courseSchedule))
            {
                return new Result { Success = true, Message = "成功创建试听！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 更新 -> 到课状态 -> 单个
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> UpdateListeningState(TrialClassUpdateListeningStateViewModel model)
        {
            var exist = await _trialClassRepository.GetModel(model.Id);
            if (null == exist)
            {
                return new Result { Success = false, Message = $"数据不存在" };
            }
            var trialClass = new TrialClass()
            {
                Id = model.Id,
                ListeningState = model.ListeningState
            };
            if (await _trialClassRepository.UpdateListeningState(trialClass) > 0)
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
            var exist = await _trialClassRepository.GetModel(id);
            if (null == exist)
            {
                return new Result { Success = false, Message = $"数据不存在" };
            }
            if (exist.ListeningState != 1)
            {
                return new Result { Success = false, Message = $"当前试听信息状态不允许进行删除操作" };
            }
            var trialClass = new TrialClass()
            {
                Id = id,
                CourseScheduleId = exist.CourseScheduleId
            };
            if (await _trialClassRepository.Delete(trialClass))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}