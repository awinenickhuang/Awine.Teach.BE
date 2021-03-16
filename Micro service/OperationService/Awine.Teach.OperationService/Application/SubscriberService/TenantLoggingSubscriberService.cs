using Awine.Framework.Core.Models;
using Awine.Teach.OperationService.Domain;
using Awine.Teach.OperationService.Domain.Interface;
using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.OperationService.Application.SubscriberService
{
    /// <summary>
    /// 租户用户登录日志
    /// </summary>
    public class TenantLoggingSubscriberService : ITenantLoggingSubscriberService, ICapSubscribe
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITenantLoggingRepository _tenantLoggingRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantLoggingRepository"></param>
        public TenantLoggingSubscriberService(ITenantLoggingRepository tenantLoggingRepository)
        {
            _tenantLoggingRepository = tenantLoggingRepository;
        }

        /// <summary>
        /// 记录登录日志
        /// </summary>
        /// <param name="log"></param>
        [CapSubscribe("acc.cdzssy.userlogon")]
        public async Task<int> ReceivedLogOnMessage(LogOnLog log)
        {
            var loginLog = new TenantLogging()
            {
                Account = log.Account,
                UserName = log.UserName,
                TenantId = log.TenantId,
                TenantName = log.TenantName,
                LogonIPAddress = log.LogonIPAddress
            };

            return await _tenantLoggingRepository.Add(loginLog);
        }
    }
}
