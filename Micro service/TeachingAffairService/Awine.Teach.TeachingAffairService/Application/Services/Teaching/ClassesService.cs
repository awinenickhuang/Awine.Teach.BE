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
    /// 班级管理
    /// </summary>
    public class ClassesService : IClassesService
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
        /// <param name="classesRepository"></param>
        /// <param name="classRoomRepository"></param>
        /// <param name="courseRepository"></param>
        /// <param name="studentRepository"></param>
        /// <param name="classCourseScheduleRepository"></param>
        /// <param name="studentCourseItemRepository"></param>
        public ClassesService(
            IMapper mapper,
            ILogger<ClassesService> logger,
            ICurrentUser user,
            IClassesRepository classesRepository,
            IClassRoomRepository classRoomRepository,
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,
            ICourseScheduleRepository classCourseScheduleRepository,
            IStudentCourseItemRepository studentCourseItemRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _classesRepository = classesRepository;
            _classRoomRepository = classRoomRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _classCourseScheduleRepository = classCourseScheduleRepository;
            _studentCourseItemRepository = studentCourseItemRepository;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="recruitStatus"></param>
        /// <param name="typeOfClass"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ClassesViewModel>> GetAll(string courseId = "", int recruitStatus = 0, int typeOfClass = 0, string beginDate = "", string finishDate = "")
        {
            var entities = await _classesRepository.GetAll(_user.TenantId, courseId, recruitStatus, typeOfClass, beginDate, finishDate);

            return _mapper.Map<IEnumerable<Classes>, IEnumerable<ClassesViewModel>>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="courseId"></param>
        /// <param name="recruitStatus"></param>
        /// <param name="typeOfClass"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        public async Task<IPagedList<ClassesViewModel>> GetPageList(int page = 1, int limit = 15, string name = "", string courseId = "", int recruitStatus = 0, int typeOfClass = 0, string beginDate = "", string finishDate = "")
        {
            var entities = await _classesRepository.GetPageList(page, limit, _user.TenantId, name, courseId, recruitStatus, typeOfClass, beginDate, finishDate);

            var result = _mapper.Map<IPagedList<Classes>, IPagedList<ClassesViewModel>>(entities);

            //计算班级学生数量

            //var students = await _studentRepository.GetAll(_user.TenantId);

            //result.Items.ToList().ForEach(x => x.StudentNumber = students.Where(s => s.ClassId == x.Id).Count());

            return result;
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ClassesViewModel> GetModel(string id)
        {
            var entities = await _classesRepository.GetModel(id);

            return _mapper.Map<Classes, ClassesViewModel>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(ClassesAddViewModel model)
        {
            if (model.TypeOfClass == 2 && model.ClassSize > 1)
            {
                return new Result { Success = false, Message = "一对一班级的班级容量只能是一人！" };
            }

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

            var entity = _mapper.Map<ClassesAddViewModel, Classes>(model);
            entity.TenantId = _user.TenantId;
            entity.CourseName = course.Name;
            entity.ClassRoomName = classRoom.Name;

            if (null != await _classesRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _classesRepository.Add(entity) > 0)
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
        public async Task<Result> Update(ClassesUpdateViewModel model)
        {
            var existClasses = await _classesRepository.GetModel(model.Id);
            if (null == existClasses)
            {
                return new Result { Success = false, Message = "未找到班级信息！" };
            }

            if (model.ClassSize < existClasses.OwnedStudents)
            {
                return new Result { Success = false, Message = "班级容量不能小于班级当前拥有的学生数量！" };
            }

            var entity = _mapper.Map<ClassesUpdateViewModel, Classes>(model);

            if (null != await _classesRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

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

            entity.ClassRoomName = classRoom.Name;
            entity.CourseName = course.Name;

            if (await _classesRepository.Update(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 更新班级招生状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> UpdateRecruitStatus(ClassesUpdateRecruitStatusViewModel model)
        {
            var existClasses = await _classesRepository.GetModel(model.Id);
            if (null == existClasses)
            {
                return new Result { Success = false, Message = "未找到班级信息！" };
            }

            if (existClasses.RecruitStatus == 3)
            {
                return new Result { Success = false, Message = "当前班级招生状态为：授课结束，不允许再修改！" };
            }

            var classes = new Classes()
            {
                Id = model.Id,
                RecruitStatus = model.RecruitStatus
            };

            if (model.RecruitStatus == 3)
            {
                //查询班级所有课节（排课信息），检查是否都已经结课
                var schedules = await _classCourseScheduleRepository.GetAll(tenantId: _user.TenantId, classId: model.Id, classStatus: 1);

                if (schedules.Count() > 0)
                {
                    return new Result { Success = false, Message = "请先对该班级的所有课节进行结课操作！" };
                }
            }

            if (await _classesRepository.UpdateRecruitStatus(classes) > 0)
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
            var classExisting = await _classesRepository.GetModel(id);

            if (null == classExisting)
            {
                return new Result { Success = false, Message = "未找到班级信息，不允许进行删除操作！" };
            }

            if (classExisting.RecruitStatus == 3)
            {
                return new Result { Success = false, Message = "班级已授课结束，不允许进行删除操作！" };
            }

            var students = await _studentCourseItemRepository.GetAll(classesId: id);

            if (students.Count() > 0)
            {
                return new Result { Success = false, Message = "班级有学生信息，不允许进行删除操作！" };
            }

            var schedules = await _classCourseScheduleRepository.GetAll(tenantId: _user.TenantId, classId: id);

            if (schedules.Count() > 0)
            {
                return new Result { Success = false, Message = "班级有排课信息，不允许进行删除操作！" };
            }

            var result = await _classesRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }

        #region 数据统计

        /// <summary>
        /// 开班数量统计
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        public async Task<BasicLineChartViewModel> ClassNumberChartReport(string designatedMonth)
        {
            var classes = await _classesRepository.GetAll(tenantId: _user.TenantId);

            DateTime time = Convert.ToDateTime(designatedMonth);

            var dateFrom = TimeCalculate.FirstDayOfMonth(time);
            classes = classes.Where(x => x.CreateTime >= dateFrom);

            var dateTo = TimeCalculate.LastDayOfMonth(time);
            classes = classes.Where(x => x.CreateTime <= dateTo);

            //当前月天数
            int days = TimeCalculate.DaysInMonth(time);

            BasicLineChartViewModel basicLineChartViewModel = new BasicLineChartViewModel();

            for (int day = 1; day <= days; day++)
            {
                basicLineChartViewModel.XAxisData.Add($"{time.Year}-{time.Month}-{day}");

                basicLineChartViewModel.SeriesData.Add(classes.Where(x => x.CreateTime.Year == time.Year && x.CreateTime.Month == time.Month && x.CreateTime.Day == day).Count());
            }

            return basicLineChartViewModel;
        }

        #endregion
    }
}
