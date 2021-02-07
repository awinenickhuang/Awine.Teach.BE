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
    /// 补课管理
    /// </summary>
    public class MakeupMissedLessonService : IMakeupMissedLessonService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<MakeupMissedLessonService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// IMakeupMissedLessonRepository
        /// </summary>
        private readonly IMakeupMissedLessonRepository _makeupMissedLessonRepository;

        /// <summary>
        /// IClassesRepository
        /// </summary>
        private readonly IClassesRepository _classesRepository;

        /// <summary>
        /// IClassRoomRepository
        /// </summary>
        private readonly IClassRoomRepository _classRoomRepository;

        /// <summary>
        /// ICourseRepository
        /// </summary>
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// IStudentRepository
        /// </summary>
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// IStudentAttendanceRepository
        /// </summary>
        private readonly IStudentAttendanceRepository _studentAttendanceRepository;

        /// <summary>
        /// IClassCourseScheduleRepository
        /// </summary>
        private readonly ICourseScheduleRepository _classCourseScheduleRepository;

        /// <summary>
        /// IStudentCourseItemRepository
        /// </summary>
        private readonly IStudentCourseItemRepository _studentCourseItemRepository;

        /// <summary>
        /// 构造 -> 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="makeupMissedLessonRepository"></param>
        /// <param name="classesRepository"></param>
        /// <param name="classRoomRepository"></param>
        /// <param name="courseRepository"></param>
        /// <param name="studentRepository"></param>
        /// <param name="studentAttendanceRepository"></param>
        /// <param name="classCourseScheduleRepository"></param>
        /// <param name="studentCourseItemRepository"></param>
        public MakeupMissedLessonService(
            IMapper mapper,
            ILogger<MakeupMissedLessonService> logger,
            ICurrentUser user,
            IMakeupMissedLessonRepository makeupMissedLessonRepository,
            IClassesRepository classesRepository,
            IClassRoomRepository classRoomRepository,
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,
            IStudentAttendanceRepository studentAttendanceRepository,
            ICourseScheduleRepository classCourseScheduleRepository,
            IStudentCourseItemRepository studentCourseItemRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _makeupMissedLessonRepository = makeupMissedLessonRepository;
            _classesRepository = classesRepository;
            _classRoomRepository = classRoomRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _studentAttendanceRepository = studentAttendanceRepository;
            _classCourseScheduleRepository = classCourseScheduleRepository;
            _studentCourseItemRepository = studentCourseItemRepository;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="status"></param>
        /// <param name="courseId"></param>
        /// <param name="teacherId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MakeupMissedLessonViewModel>> GetAll(int status = 0, string courseId = "", string teacherId = "", string beginDate = "", string endDate = "")
        {
            var entities = await _makeupMissedLessonRepository.GetAll(_user.TenantId, status, courseId, teacherId, beginDate, endDate);

            return _mapper.Map<IEnumerable<MakeupMissedLesson>, IEnumerable<MakeupMissedLessonViewModel>>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="status"></param>
        /// <param name="courseId"></param>
        /// <param name="teacherId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPagedList<MakeupMissedLessonViewModel>> GetPageList(int page = 1, int limit = 15, int status = 0, string courseId = "", string teacherId = "", string beginDate = "", string endDate = "")
        {
            var entities = await _makeupMissedLessonRepository.GetPageList(page, limit, _user.TenantId, status, courseId, teacherId, beginDate, endDate);

            var result = _mapper.Map<IPagedList<MakeupMissedLesson>, IPagedList<MakeupMissedLessonViewModel>>(entities);

            return result;
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MakeupMissedLessonViewModel> GetModel(string id)
        {
            var entity = await _makeupMissedLessonRepository.GetModel(id);

            return _mapper.Map<MakeupMissedLesson, MakeupMissedLessonViewModel>(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(MakeupMissedLessonAddViewModel model)
        {
            var entity = _mapper.Map<MakeupMissedLessonAddViewModel, MakeupMissedLesson>(model);

            DateTime classOpeningDate = Convert.ToDateTime(model.ClassOpeningDate);

            if (classOpeningDate.Year != DateTime.Now.ToLocalTime().Year)
            {
                return new Result { Success = false, Message = "只能安排当前年份的补课计划！" };
            }

            entity.Status = 1;
            entity.CourseDates = classOpeningDate;
            entity.TenantId = _user.TenantId;

            var course = await _courseRepository.GetModel(model.CourseId);

            if (null == course)
            {
                return new Result { Success = true, Message = "未找到课程信息！" };
            }

            entity.CourseName = course.Name;

            var classRoom = await _classRoomRepository.GetModel(model.ClassRoomId);

            if (null == classRoom)
            {
                return new Result { Success = true, Message = "未找到教室信息！" };
            }

            entity.ClassRoom = classRoom.Name;

            var courseSchedule = new CourseSchedule()
            {
                CreateTime = entity.CreateTime,
                Year = DateTime.Now.Year,
                CourseId = entity.CourseId,
                CourseName = entity.CourseName,
                ClassId = entity.Id,
                ClassName = entity.Name,
                TeacherId = entity.TeacherId,
                TeacherName = entity.TeacherName,
                ClassRoomId = entity.ClassRoomId,
                ClassRoom = entity.ClassRoom,
                CourseDates = entity.CourseDates,
                StartHours = entity.StartHours,
                StartMinutes = entity.StartMinutes,
                EndHours = entity.EndHours,
                EndMinutes = entity.EndMinutes,
                ClassStatus = 1,
                ScheduleIdentification = 3,
                TenantId = _user.TenantId
            };

            entity.ClassCourseScheduleId = courseSchedule.Id;

            if (await _makeupMissedLessonRepository.Add(entity, courseSchedule))
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
        public async Task<Result> Update(MakeupMissedLessonUpdateViewModel model)
        {
            DateTime classOpeningDate = Convert.ToDateTime(model.ClassOpeningDate);

            if (classOpeningDate.Year != DateTime.Now.ToLocalTime().Year)
            {
                return new Result { Success = false, Message = "只能安排当前年份的补课计划！" };
            }

            var exist = await _makeupMissedLessonRepository.GetModel(model.Id);

            if (null == exist)
            {
                return new Result { Success = false, Message = "操作失败，未找到补课信息！" };
            }

            if (exist.Status == 2)
            {
                return new Result { Success = false, Message = "补课已结课，不允许再修改了 ^_^ ！" };
            }

            var entity = _mapper.Map<MakeupMissedLessonUpdateViewModel, MakeupMissedLesson>(model);

            entity.CourseDates = classOpeningDate;

            var course = await _courseRepository.GetModel(model.CourseId);

            if (null == course)
            {
                return new Result { Success = true, Message = "未找到课程信息！" };
            }

            entity.CourseName = course.Name;

            var classRoom = await _classRoomRepository.GetModel(model.ClassRoomId);

            if (null == classRoom)
            {
                return new Result { Success = true, Message = "未找到教室信息！" };
            }

            entity.ClassRoom = classRoom.Name;

            var courseSchedule = new CourseSchedule()
            {
                Id = exist.ClassCourseScheduleId,
                Year = DateTime.Now.Year,
                CourseId = entity.CourseId,
                CourseName = entity.CourseName,
                ClassId = entity.Id,
                ClassName = entity.Name,
                TeacherId = entity.TeacherId,
                TeacherName = entity.TeacherName,
                ClassRoomId = entity.ClassRoomId,
                ClassRoom = entity.ClassRoom,
                CourseDates = entity.CourseDates,
                StartHours = entity.StartHours,
                StartMinutes = entity.StartMinutes,
                EndHours = entity.EndHours,
                EndMinutes = entity.EndMinutes,
                ClassStatus = 1,
                ScheduleIdentification = 3,
                TenantId = _user.TenantId,
                CreateTime = entity.CreateTime
            };

            if (await _makeupMissedLessonRepository.Update(entity, courseSchedule))
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
            var exist = await _makeupMissedLessonRepository.GetModel(id);
            if (null == exist)
            {
                return new Result { Success = false, Message = "未找到补课信息！" };
            }

            if (exist.Status == 2)
            {
                return new Result { Success = false, Message = "补课已结课，不能再删除了！" };
            }

            //有学生也不能删

            if (await _makeupMissedLessonRepository.Delete(id))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 补课学生分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="makeupMissedLessonId">补课班级ID</param>
        /// <param name="classCourseScheduleId">补课班级课表ID</param>
        /// <returns></returns>
        public async Task<IPagedList<MakeupMissedLessonStudentViewModel>> GetMakeupMissedLessonStudentPageList(int page = 1, int limit = 15, string makeupMissedLessonId = "", string classCourseScheduleId = "")
        {
            var entities = await _makeupMissedLessonRepository.GetMakeupMissedLessonStudentPageList(page, limit, _user.TenantId, makeupMissedLessonId, classCourseScheduleId);

            var result = _mapper.Map<IPagedList<MakeupMissedLessonStudent>, IPagedList<MakeupMissedLessonStudentViewModel>>(entities);

            return result;
        }

        /// <summary>
        /// 添加补课学生
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> AddStudentToClass(MakeupMissedLessonStudentAddViewModel model)
        {
            if (model.MksAttendances.Count <= 0)
            {
                return new Result { Success = false, Message = "操作失败，未选择任何数据！" };
            }

            IList<MakeupMissedLessonStudent> students = new List<MakeupMissedLessonStudent>();
            IList<StudentAttendance> studentAttendances = new List<StudentAttendance>();

            var classes = await _makeupMissedLessonRepository.GetModel(model.MakeupMissedLessonId);

            if (null == classes)
            {
                return new Result { Success = false, Message = "操作失败，未到到补课班级信息！" };
            }

            if (classes.Status == 2)
            {
                return new Result { Success = false, Message = "补课已结课，不能再添加学生了！" };
            }

            foreach (MksAttendance item in model.MksAttendances)
            {
                var attendance = await _studentAttendanceRepository.GetModel(item.AttendanceId);

                if (null == attendance)
                {
                    return new Result { Success = false, Message = "操作失败，数据选择有误，请刷新后重试！" };
                }

                var courseItem = await _studentCourseItemRepository.GetModel(attendance.StudentCourseItemId);

                if (null == courseItem)
                {
                    return new Result { Success = false, Message = "操作失败，未找到报读课程信息！" };
                }

                students.Add(new MakeupMissedLessonStudent()
                {
                    StudentId = attendance.StudentId,
                    StudentCourseItemId = courseItem.Id,
                    ClassCourseScheduleId = classes.ClassCourseScheduleId,
                    AttendanceId = attendance.Id,
                    MakeupMissedLessonId = model.MakeupMissedLessonId,
                    TenantId = _user.TenantId
                });

                studentAttendances.Add(new StudentAttendance()
                {
                    Id = attendance.Id,
                    ProcessingStatus = 3
                });
            }

            if (await _makeupMissedLessonRepository.AddStudentToClass(students, studentAttendances))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 移除补课学生
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> RemoveStudentFromClass(MakeupMissedLessonStudentRemoveViewModel model)
        {
            if (model.MksStudents.Count <= 0)
            {
                return new Result { Success = false, Message = "操作失败，未选择任何数据！" };
            }

            IList<MakeupMissedLessonStudent> students = new List<MakeupMissedLessonStudent>();
            IList<StudentAttendance> studentAttendances = new List<StudentAttendance>();

            var classes = await _makeupMissedLessonRepository.GetModel(model.MakeupMissedLessonId);
            if (null == classes)
            {
                return new Result { Success = false, Message = "操作失败，未到到补课班级信息！" };
            }

            if (classes.Status == 2)
            {
                return new Result { Success = false, Message = "补课已结课，不能再移除学生了！" };
            }

            foreach (MksStudent item in model.MksStudents)
            {
                var attendance = await _studentAttendanceRepository.GetModel(item.AttendanceId);

                if (null == attendance)
                {
                    return new Result { Success = false, Message = "操作失败，数据选择有误，请刷新后重试！" };
                }

                students.Add(new MakeupMissedLessonStudent()
                {
                    Id = item.Id
                });

                studentAttendances.Add(new StudentAttendance()
                {
                    Id = item.AttendanceId,
                    ProcessingStatus = 2,
                });
            }

            if (await _makeupMissedLessonRepository.RemoveStudentFromClass(students, studentAttendances))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
