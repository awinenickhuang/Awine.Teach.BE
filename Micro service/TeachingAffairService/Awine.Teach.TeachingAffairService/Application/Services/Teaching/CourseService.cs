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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application
{
    /// <summary>
    /// 课程
    /// </summary>
    public class CourseService : ICourseService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<CourseService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// ICourseRepository
        /// </summary>
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// ICourseChargeMannerRepository
        /// </summary>
        private readonly ICourseChargeMannerRepository _courseChargeMannerRepository;

        /// <summary>
        /// ICourseScheduleRepository
        /// </summary>
        private readonly ICourseScheduleRepository _courseScheduleRepository;

        /// <summary>
        /// 构造 - > 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="courseRepository"></param>
        /// <param name="courseChargeMannerRepository"></param>
        /// <param name="courseScheduleRepository"></param>
        public CourseService(
            IMapper mapper,
            ILogger<CourseService> logger,
            ICurrentUser user,
            ICourseRepository courseRepository,
            ICourseChargeMannerRepository courseChargeMannerRepository,
            ICourseScheduleRepository courseScheduleRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _courseRepository = courseRepository;
            _courseChargeMannerRepository = courseChargeMannerRepository;
            _courseScheduleRepository = courseScheduleRepository;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="enabledStatus"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseViewModel>> GetAll(int enabledStatus = 0)
        {
            var entities = await _courseRepository.GetAll(_user.TenantId, enabledStatus);

            return _mapper.Map<IEnumerable<Course>, IEnumerable<CourseViewModel>>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="enabledStatus"></param>
        /// <returns></returns>
        public async Task<IPagedList<CourseViewModel>> GetPageList(int page = 1, int limit = 15, int enabledStatus = 0)
        {
            var entities = await _courseRepository.GetPageList(page, limit, _user.TenantId, enabledStatus);

            return _mapper.Map<IPagedList<Course>, IPagedList<CourseViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(CourseAddViewModel model)
        {
            var entity = _mapper.Map<CourseAddViewModel, Course>(model);
            entity.TenantId = _user.TenantId;

            if (null != await _courseRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _courseRepository.Add(entity) > 0)
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
        public async Task<Result> Update(CourseUpdateViewModel model)
        {
            var entity = _mapper.Map<CourseUpdateViewModel, Course>(model);

            if (null != await _courseRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _courseRepository.Update(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 更新 -> 课程状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> UpdateEnableStatus(CourseUpdateEnableStatusViewModel model)
        {
            var course = await _courseRepository.GetModel(model.Id);
            if (null == course)
            {
                return new Result { Success = false, Message = "操作失败，找不到课程信息！" };
            }
            course.EnabledStatus = model.EnabledStatus;

            var result = await _courseRepository.UpdateEnableStatus(course);

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
            var chargeManner = await _courseChargeMannerRepository.GetAll(_user.TenantId, id);
            if (chargeManner.Count() > 0)
            {
                return new Result { Success = false, Message = "操作失败：课程设置了定价标准，请先删除定价标准！" };
            }

            var courseSchedule = await _courseScheduleRepository.GetAll(courseId: id);
            if (courseSchedule.Count() > 0)
            {
                return new Result { Success = false, Message = "操作失败：课程安排了课节信息，不允许删除！" };
            }

            var result = await _courseRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
