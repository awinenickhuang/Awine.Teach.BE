using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// 租户信息
    /// </summary>
    public interface ITenantsService
    {
        /// <summary>
        /// 全部数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TenantsViewModel>> GetAll();

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IPagedList<TenantsViewModel>> GetPageList(int page = 1, int limit = 15);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TenantsViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(TenantsAddViewModel model);

        /// <summary>
        /// 更新 -> 基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(TenantsUpdateViewModel model);

        /// <summary>
        /// 更新 -> 租户类型 1-免费 2-试用 3-付费（VIP）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdateClassiFication(TenantsUpdateClassiFicationViewModel model);

        /// <summary>
        /// 更新 -> 租户状态 1-正常 2-锁定（异常）3-锁定（过期）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdateStatus(TenantsUpdateStatusViewModel model);

        /// <summary>
        /// 更新 -> 允许添加的分支机构个数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdateNumberOfBranches(TenantsUpdateNumberOfBranchesViewModel model);
    }
}
