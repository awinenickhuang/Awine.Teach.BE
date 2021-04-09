using AutoMapper;
using Awine.Framework.Core.Collections;
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
    /// 机构信息设置 不同的SaaS版本对应不同的配置
    /// </summary>
    public class TenantDefaultSettingsService : ITenantDefaultSettingsService
    {
        /// <summary>
        /// 机构设置
        /// </summary>
        private readonly ITenantDefaultSettingsRepository _tenantDefaultSettingsRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<TenantDefaultSettingsService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tenantDefaultSettingsRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public TenantDefaultSettingsService(ITenantDefaultSettingsRepository tenantDefaultSettingsRepository,
            IMapper mapper,
            ILogger<TenantDefaultSettingsService> logger)
        {
            _tenantDefaultSettingsRepository = tenantDefaultSettingsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<TenantDefaultSettingsViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _tenantDefaultSettingsRepository.GetPageList(page, limit);

            return _mapper.Map<IPagedList<TenantDefaultSettings>, IPagedList<TenantDefaultSettingsViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TenantDefaultSettingsViewModel>> GetAll()
        {
            var entities = await _tenantDefaultSettingsRepository.GetAll();

            return _mapper.Map<IEnumerable<TenantDefaultSettings>, IEnumerable<TenantDefaultSettingsViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TenantDefaultSettingsViewModel> GetModel(string id)
        {
            var entity = await _tenantDefaultSettingsRepository.GetModel(id);

            return _mapper.Map<TenantDefaultSettings, TenantDefaultSettingsViewModel>(entity);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        public async Task<TenantDefaultSettingsViewModel> GetModelForAppVersion(string saaSVersionId)
        {
            var entity = await _tenantDefaultSettingsRepository.GetModelForAppVersion(saaSVersionId);

            return _mapper.Map<TenantDefaultSettings, TenantDefaultSettingsViewModel>(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(TenantDefaultSettingsAddViewModel model)
        {
            var entity = _mapper.Map<TenantDefaultSettingsAddViewModel, TenantDefaultSettings>(model);
            var existing = await _tenantDefaultSettingsRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }

            var result = await _tenantDefaultSettingsRepository.Add(entity);

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
        public async Task<Result> Update(TenantDefaultSettingsUpdateViewModel model)
        {
            var entity = _mapper.Map<TenantDefaultSettingsUpdateViewModel, TenantDefaultSettings>(model);
            var existing = await _tenantDefaultSettingsRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }

            var result = await _tenantDefaultSettingsRepository.Update(entity);

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
            var deleteResult = await _tenantDefaultSettingsRepository.Delete(id);

            if (deleteResult > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
