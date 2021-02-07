using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 角色权限 -> 角色拥有的模块信息 -> 视图模型
    /// </summary>
    public class RolesOwnedModulesViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 模块|菜单标识
        /// </summary>
        public string ModuleId { get; set; }
    }
}
