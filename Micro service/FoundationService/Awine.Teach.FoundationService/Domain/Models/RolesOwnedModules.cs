using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 角色权限 -> 角色拥有的模块信息
    /// </summary>
    public class RolesOwnedModules
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 模块|菜单标识
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }
    }
}
