using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.OperationService.Domain.Interface
{
    /// <summary>
    /// 租户用户登录
    /// </summary>
    public interface ITenantLoggingRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="account"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IPagedList<TenantLogging>> GetPageList(int page = 1, int limit = 15, string account = "", string tenantId = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(TenantLogging model);
    }
}
