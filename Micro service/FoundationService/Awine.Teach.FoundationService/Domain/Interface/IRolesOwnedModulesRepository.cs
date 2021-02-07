using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// 角色权限 -> 角色拥有的模块信息
    /// </summary>
    public interface IRolesOwnedModulesRepository
    {
        /// <summary>
        /// 保存角色操作权限 -> 模块及按钮信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="aspnetrolesOwnedModules"></param>
        /// <param name="aspnetroleClaims"></param>
        /// <returns></returns>
        Task<bool> SaveRoleOwnedModules(string roleId, IList<RolesOwnedModules> aspnetrolesOwnedModules, IList<RolesClaims> aspnetroleClaims);

        /// <summary>
        /// 查询 -> 角色拥有的模块信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IEnumerable<RolesOwnedModules>> GetRoleOwnedModules(string roleId);

        /// <summary>
        /// 获取模块集合，以识别模块是否被角色权限使用
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<IEnumerable<RolesOwnedModules>> GetModels(string moduleId);
    }
}
