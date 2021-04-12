using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// SaaS版本
    /// </summary>
    public interface ISaaSVersionRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        Task<IPagedList<SaaSVersion>> GetPageList(int page = 1, int limit = 15, string name = "", string identifying = "");

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        Task<IEnumerable<SaaSVersion>> GetAll(string name = "", string identifying = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tenantsDefaultSettingsModel"></param>
        /// <returns></returns>
        Task<bool> Add(SaaSVersion model, TenantDefaultSettings tenantsDefaultSettingsModel);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(SaaSVersion model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string id);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SaaSVersion> GetModel(string id);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<SaaSVersion> GetModel(SaaSVersion model);
    }
}
