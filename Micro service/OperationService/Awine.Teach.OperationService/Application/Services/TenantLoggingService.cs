using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Teach.OperationService.Application.Interfaces;
using Awine.Teach.OperationService.Application.ViewModels;
using Awine.Teach.OperationService.Domain;
using Awine.Teach.OperationService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.OperationService.Application.Services
{
    /// <summary>
    /// 租户用户登录
    /// </summary>
    public class TenantLoggingService : ITenantLoggingService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<TenantLoggingService> _logger;

        /// <summary>
        /// ITenantLoggingRepository
        /// </summary>
        private readonly ITenantLoggingRepository _tenantLoggingRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="tenantLoggingRepository"></param>
        public TenantLoggingService(
            IMapper mapper,
            ILogger<TenantLoggingService> logger,
            ITenantLoggingRepository tenantLoggingRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _tenantLoggingRepository = tenantLoggingRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="account"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IPagedList<TenantLoggingViewModel>> GetPageList(int page = 1, int limit = 15, string account = "", string tenantId = "")
        {
            var entities = await _tenantLoggingRepository.GetPageList(page, limit, account, tenantId);

            return _mapper.Map<IPagedList<TenantLogging>, IPagedList<TenantLoggingViewModel>>(entities);
        }
    }
}
