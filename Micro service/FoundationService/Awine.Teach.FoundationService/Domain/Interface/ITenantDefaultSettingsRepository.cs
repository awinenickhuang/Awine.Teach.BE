using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// 机构信息设置 不同的SaaS版本对应不同的配置
    /// </summary>
    public interface ITenantDefaultSettingsRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IPagedList<TenantDefaultSettings>> GetPageList(int page = 1, int limit = 15);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        Task<IEnumerable<TenantDefaultSettings>> GetAll(string saaSVersionId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TenantDefaultSettings> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        Task<TenantDefaultSettings> GetModelForAppVersion(string saaSVersionId);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TenantDefaultSettings> GetModel(TenantDefaultSettings model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(TenantDefaultSettings model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(TenantDefaultSettings model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
