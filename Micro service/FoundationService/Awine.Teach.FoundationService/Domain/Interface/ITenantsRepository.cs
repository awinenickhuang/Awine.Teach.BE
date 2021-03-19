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
        /// <returns></returns>
        Task<IEnumerable<Tenants>> GetAll(string tenantId);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IPagedList<Tenants>> GetPageList(int page = 1, int limit = 15, string tenantId = "");

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Tenants> GetModel(string id);

        /// <summary>
        /// 取某一类型租户
        /// </summary>
        /// <param name="classiFication"></param>
        /// <returns></returns>
        Task<IEnumerable<Tenants>> GetClassiFication(int classiFication);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Tenants> GetModel(Tenants model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(Tenants model);

        /// <summary>
        /// 更新 -> 基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(Tenants model);

        /// <summary>
        /// 更新 -> 租户类型 1-免费 2-试用 3-付费（VIP）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateClassiFication(Tenants model);

        /// <summary>
        /// 更新 -> 租户状态 1-正常 2-锁定（异常）3-锁定（过期）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateStatus(Tenants model);

        /// <summary>
        /// 更新 -> 允许添加的分支机构个数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateNumberOfBranches(Tenants model);

        /// <summary>
        /// 入驻 -> 注册
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <param name="userModel"></param>
        /// <param name="rolesModel"></param>
        /// <param name="rolesOwnedModules"></param>
        /// <returns></returns>
        Task<bool> Enter(Tenants tenantModel, Users userModel, Roles rolesModel, IList<RolesOwnedModules> rolesOwnedModules);
    }
}
