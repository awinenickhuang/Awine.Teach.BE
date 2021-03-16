using Awine.Framework.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.OperationService.Application.SubscriberService
{
    /// <summary>
    /// 租户用户登录日志
    /// </summary>
    public interface ITenantLoggingSubscriberService
    {
        /// <summary>
        /// 记录登录日志
        /// </summary>
        /// <param name="log"></param>
        Task<int> ReceivedLogOnMessage(LogOnLog log);
    }
}
