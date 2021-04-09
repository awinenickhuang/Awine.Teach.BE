using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// SaaS版本定价策略
    /// </summary>
    public interface ISaaSPricingTacticsRepository
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        Task<IEnumerable<SaaSPricingTactics>> GetAll(string saaSVersionId);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        Task<IPagedList<SaaSPricingTactics>> GetPageList(int page = 1, int limit = 15, string saaSVersionId = "");

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SaaSPricingTactics> GetModel(string id);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<SaaSPricingTactics> GetModel(SaaSPricingTactics model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(SaaSPricingTactics model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(SaaSPricingTactics model);
    }
}
