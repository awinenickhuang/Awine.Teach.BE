using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// 租户信息
    /// </summary>
    public interface ITenantsRepository
    {
        /// <summary>
        /// 全部数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="classiFication"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="status"></param>
        /// <param name="industryId"></param>
        /// <param name="creatorId"></param>
        /// <param name="creatorTenantId"></param>
        /// <returns></returns>
        Task<IEnumerable<Tenants>> GetAll(string tenantId = "", int classiFication = 0, string saaSVersionId = "", int status = 0, string industryId = "", string creatorId = "", string creatorTenantId = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="classiFication"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="status"></param>
        /// <param name="industryId"></param>
        /// <param name="creatorId"></param>
        /// <param name="creatorTenantId"></param>
        /// <returns></returns>
        Task<IPagedList<Tenants>> GetPageList(int page = 1, int limit = 15, string tenantId = "", int classiFication = 0, string saaSVersionId = "", int status = 0, string industryId = "", string creatorId = "", string creatorTenantId = "");

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Tenants> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Tenants> GetModel(Tenants model);

        /// <summary>
        /// 租户开通
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="department"></param>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <param name="rolesOwnedModules"></param>
        /// <param name="tenantSettings"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<bool> Add(Tenants tenant, Departments department, Users user, Roles role, IList<RolesOwnedModules> rolesOwnedModules, TenantSettings tenantSettings, Orders order);

        /// <summary>
        /// 更新 -> 基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(Tenants model);

        /// <summary>
        /// 更新 -> 租户状态 1-正常 2-锁定（异常）3-锁定（过期）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateStatus(Tenants model);
    }
}
