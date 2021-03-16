using Awine.Framework.Core.Collections;
using Awine.Teach.OperationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.OperationService.Application.Interfaces
{
    /// <summary>
    /// 租户用户登录
    /// </summary>
    public interface ITenantLoggingService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="account"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IPagedList<TenantLoggingViewModel>> GetPageList(int page = 1, int limit = 15, string account = "", string tenantId = "");
    }
}
