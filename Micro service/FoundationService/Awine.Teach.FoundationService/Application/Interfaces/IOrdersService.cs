using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public interface IOrdersService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="tradeCategories"></param>
        /// <param name="performanceOwnerId"></param>
        /// <param name="performanceTenantId"></param>
        /// <returns></returns>
        Task<IEnumerable<OrdersViewModel>> GetAll(string tenantId = "", string saaSVersionId = "", int tradeCategories = 0, string performanceOwnerId = "", string performanceTenantId = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="tradeCategories"></param>
        /// <param name="performanceOwnerId"></param>
        /// <param name="performanceTenantId"></param>
        /// <returns></returns>
        Task<IPagedList<OrdersViewModel>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string saaSVersionId = "", int tradeCategories = 0, string performanceOwnerId = "", string performanceTenantId = "");

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //Task<bool> Add(Orders model);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OrdersViewModel> GetModel(string id);
    }
}
