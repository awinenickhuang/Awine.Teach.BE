using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// 租户信息设置
    /// </summary>
    public interface ITenantSettingsRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IPagedList<TenantSettings>> GetPageList(int page = 1, int limit = 15, string tenantId = "");

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IEnumerable<TenantSettings>> GetAll(string tenantId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<TenantSettings> GetTenantSettings(string tenantId);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TenantSettings> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TenantSettings> GetModel(TenantSettings model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(TenantSettings model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(TenantSettings model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
