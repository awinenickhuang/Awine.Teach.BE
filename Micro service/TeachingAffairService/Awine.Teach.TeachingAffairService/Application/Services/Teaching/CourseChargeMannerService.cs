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
    /// 课程收费方式
    /// </summary>
    public class CourseChargeMannerService : ICourseChargeMannerService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<CourseChargeMannerService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// ICourseChargeMannerRepository
        /// </summary>
        private readonly ICourseChargeMannerRepository _courseChargeMannerRepository;

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
        /// <param name="courseChargeMannerRepository"></param>
        /// <param name="studentCourseItemRepository"></param>
        public CourseChargeMannerService(
            IMapper mapper,
            ILogger<CourseChargeMannerService> logger,
            ICurrentUser user,
            ICourseChargeMannerRepository courseChargeMannerRepository,
            IStudentCourseItemRepository studentCourseItemRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _courseChargeMannerRepository = courseChargeMannerRepository;
            _studentCourseItemRepository = studentCourseItemRepository;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseChargeMannerViewModel>> GetAll(string courseId = "")
        {
            var entities = await _courseChargeMannerRepository.GetAll(_user.TenantId, courseId);

            return _mapper.Map<IEnumerable<CourseChargeManner>, IEnumerable<CourseChargeMannerViewModel>>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<IPagedList<CourseChargeMannerViewModel>> GetPageList(int page = 1, int limit = 15, string courseId = "")
        {
            var entities = await _courseChargeMannerRepository.GetPageList(page, limit, _user.TenantId, courseId);

            return _mapper.Map<IPagedList<CourseChargeManner>, IPagedList<CourseChargeMannerViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CourseChargeMannerViewModel> GetModel(string id)
        {
            var entiy = await _courseChargeMannerRepository.GetModel(id);
            return _mapper.Map<CourseChargeManner, CourseChargeMannerViewModel>(entiy);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(CourseChargeMannerAddViewModel model)
        {
            var entity = _mapper.Map<CourseChargeMannerAddViewModel, CourseChargeManner>(model);

            entity.TenantId = _user.TenantId;

            var existing = await _courseChargeMannerRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "已添加过相同的定价标准！" };
            }

            if (await _courseChargeMannerRepository.Add(entity) > 0)
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
        public async Task<Result> Update(CourseChargeMannerUpdateViewModel model)
        {
            var entity = _mapper.Map<CourseChargeMannerUpdateViewModel, CourseChargeManner>(model);

            var existing = await _courseChargeMannerRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "已添加过相同的定价标准！" };
            }

            if (await _courseChargeMannerRepository.Update(entity) > 0)
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
        public async Task<int> Delete(string id)
        {
            return await _courseChargeMannerRepository.Delete(id);
        }
    }
}
