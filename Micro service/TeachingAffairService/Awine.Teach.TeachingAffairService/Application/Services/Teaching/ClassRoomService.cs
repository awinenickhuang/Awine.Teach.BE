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

namespace Awine.Teach.TeachingAffairService.Application.Services
{
    /// <summary>
    /// 教室管理
    /// </summary>
    public class ClassRoomService : IClassRoomService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ClassRoomService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// IClassRoomRepository
        /// </summary>
        private readonly IClassRoomRepository _classRoomRepository;

        /// <summary>
        /// IClassCourseScheduleRepository
        /// </summary>
        private readonly ICourseScheduleRepository _classCourseScheduleRepository;

        /// <summary>
        /// 构造 -> 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="classRoomRepository"></param>
        /// <param name="classCourseScheduleRepository"></param>
        public ClassRoomService(
            IMapper mapper,
            ILogger<ClassRoomService> logger,
            ICurrentUser user,
            IClassRoomRepository classRoomRepository,
            ICourseScheduleRepository classCourseScheduleRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _classRoomRepository = classRoomRepository;
            _classCourseScheduleRepository = classCourseScheduleRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<ClassRoomViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _classRoomRepository.GetPageList(page, limit, _user.TenantId);

            return _mapper.Map<IPagedList<ClassRoom>, IPagedList<ClassRoomViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ClassRoomViewModel>> GetAll()
        {
            var entities = await _classRoomRepository.GetAll(_user.TenantId);

            return _mapper.Map<IEnumerable<ClassRoom>, IEnumerable<ClassRoomViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(ClassRoomAddViewModel model)
        {
            var entity = _mapper.Map<ClassRoomAddViewModel, ClassRoom>(model);

            entity.TenantId = _user.TenantId;

            if (null != await _classRoomRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _classRoomRepository.Add(entity) > 0)
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
        public async Task<Result> Update(ClassRoomUpdateViewModel model)
        {
            var entity = _mapper.Map<ClassRoomUpdateViewModel, ClassRoom>(model);

            if (null != await _classRoomRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _classRoomRepository.Update(entity) > 0)
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
            var courseSchedule = await _classCourseScheduleRepository.GetAll(classRoomId: id);

            if (courseSchedule.Count() > 0)
            {
                return new Result { Success = false, Message = "操作失败，班级已被课节使用！" };
            }

            var result = await _classRoomRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
