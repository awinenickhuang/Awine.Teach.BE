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
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Services
{
    /// <summary>
    /// 班级相册
    /// </summary>
    public class ClassPhotoalbumService : IClassPhotoalbumService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ClassPhotoalbumService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// IClassPhotoalbumRepository
        /// </summary>
        private readonly IClassPhotoalbumRepository _classPhotoalbumRepository;

        /// <summary>
        /// 构造 -> 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="classPhotoalbumRepository"></param>
        public ClassPhotoalbumService(
            IMapper mapper,
            ILogger<ClassPhotoalbumService> logger,
            ICurrentUser user,
            IClassPhotoalbumRepository classPhotoalbumRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _classPhotoalbumRepository = classPhotoalbumRepository;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="visibleRange"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ClassPhotoalbumViewModel>> GetAll(string classId = "", int visibleRange = 0, string startDate = "", string endDate = "")
        {
            var entities = await _classPhotoalbumRepository.GetAll(_user.TenantId, classId, visibleRange, startDate, endDate);

            return _mapper.Map<IEnumerable<ClassPhotoalbum>, IEnumerable<ClassPhotoalbumViewModel>>(entities);
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="classId"></param>
        /// <param name="visibleRange"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPagedList<ClassPhotoalbumViewModel>> GetPageList(int page = 1, int limit = 15, string classId = "", int visibleRange = 0, string startDate = "", string endDate = "")
        {
            var entities = await _classPhotoalbumRepository.GetPageList(page, limit, _user.TenantId, classId, visibleRange, startDate, endDate);

            return _mapper.Map<IPagedList<ClassPhotoalbum>, IPagedList<ClassPhotoalbumViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(ClassPhotoalbumAddViewModel model)
        {
            var entity = _mapper.Map<ClassPhotoalbumAddViewModel, ClassPhotoalbum>(model);
            entity.TenantId = _user.TenantId;

            if (null != await _classPhotoalbumRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _classPhotoalbumRepository.Add(entity) > 0)
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
            if (await _classPhotoalbumRepository.Delete(id))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 取一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ClassPhotoalbumViewModel> GetModel(string id)
        {
            var entities = await _classPhotoalbumRepository.GetModel(id);

            return _mapper.Map<ClassPhotoalbum, ClassPhotoalbumViewModel>(entities);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(ClassPhotoalbumUpdateViewModel model)
        {
            var entity = _mapper.Map<ClassPhotoalbumUpdateViewModel, ClassPhotoalbum>(model);
            entity.TenantId = _user.TenantId;

            if (null != await _classPhotoalbumRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _classPhotoalbumRepository.Update(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
