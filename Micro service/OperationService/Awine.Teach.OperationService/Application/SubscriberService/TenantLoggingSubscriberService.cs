using Awine.Framework.Core.Models;
using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.OperationService.Application.SubscriberService
{
    /// <summary>
    /// 租户用户登录日志
    /// </summary>
    public class TenantLoggingSubscriberService : ITenantLoggingSubscriberService, ICapSubscribe
    {
        /// <summary>
        /// 记录登录日志
        /// </summary>
        /// <param name="log"></param>
        [CapSubscribe("acc.cdzssy.userlogon")]
        public void ReceivedLogOnMessage(LogOnLog log)
        {

        }
    }
}
