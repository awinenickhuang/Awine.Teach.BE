using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public interface IOrdersRepository
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
        Task<IEnumerable<Orders>> GetAll(string tenantId = "", string saaSVersionId = "", int tradeCategories = 0, string performanceOwnerId = "", string performanceTenantId = "");

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
        Task<IPagedList<Orders>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string saaSVersionId = "", int tradeCategories = 0, string performanceOwnerId = "", string performanceTenantId = "");

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Add(Orders model);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Orders> GetModel(string id);
    }
}
