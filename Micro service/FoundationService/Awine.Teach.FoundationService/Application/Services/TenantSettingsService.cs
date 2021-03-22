using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Application.ViewModels;
using Awine.Teach.FoundationService.Domain.Interface;
using Awine.Teach.FoundationService.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Services
{
    /// <summary>
    /// 机构设置
    /// </summary>
    public class TenantSettingsService : ITenantSettingsService
    {
        /// <summary>
        /// 机构设置
        /// </summary>
        private readonly ITenantSettingsRepository _tenantSettingsRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<TenantSettingsService> _logger;

        /// <summary>
        /// 当前用户
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tenantSettingsRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        public TenantSettingsService(ITenantSettingsRepository tenantSettingsRepository, IMapper mapper, ILogger<TenantSettingsService> logger,
            ICurrentUser user)
        {
            _tenantSettingsRepository = tenantSettingsRepository;
            _mapper = mapper;
            _logger = logger;
            _user = user;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<TenantSettingsViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _tenantSettingsRepository.GetPageList(page, limit, _user.TenantId);

            return _mapper.Map<IPagedList<TenantSettings>, IPagedList<TenantSettingsViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TenantSettingsViewModel>> GetAll()
        {
            var entities = await _tenantSettingsRepository.GetAll(_user.TenantId);

            return _mapper.Map<IEnumerable<TenantSettings>, IEnumerable<TenantSettingsViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TenantSettingsViewModel> GetModel(string id)
        {
            var entity = await _tenantSettingsRepository.GetModel(id);

            return _mapper.Map<TenantSettings, TenantSettingsViewModel>(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(TenantSettingsAddViewModel model)
        {
            var entity = _mapper.Map<TenantSettingsAddViewModel, TenantSettings>(model);
            entity.TenantId = _user.TenantId;
            var existing = await _tenantSettingsRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }

            var result = await _tenantSettingsRepository.Add(entity);

            if (result > 0)
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
        public async Task<Result> Update(TenantSettingsUpdateViewModel model)
        {
            var entity = _mapper.Map<TenantSettingsUpdateViewModel, TenantSettings>(model);
            entity.TenantId = _user.TenantId;
            var existing = await _tenantSettingsRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }

            var result = await _tenantSettingsRepository.Update(entity);

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
            var deleteResult = await _tenantSettingsRepository.Delete(id);

            if (deleteResult > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
