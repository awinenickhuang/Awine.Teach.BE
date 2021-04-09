using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// SaaS版本定价策略
    /// </summary>
    public interface ISaaSPricingTacticsService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        Task<IEnumerable<SaaSPricingTacticsViewModel>> GetAll(string saaSVersionId);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        Task<IPagedList<SaaSPricingTacticsViewModel>> GetPageList(int page = 1, int limit = 15, string saaSVersionId = "");

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SaaSPricingTacticsViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(SaaSPricingTacticsAddViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(SaaSPricingTacticsUpdateViewModel model);
    }
}
