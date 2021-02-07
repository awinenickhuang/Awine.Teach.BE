using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 角色权限 -> 角色拥有的模块信息 -> 保存 -> 视图模型
    /// </summary>
    public class RolesOwnedModulesAddViewModel
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "角色标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "角色标识不正确")]
        public string RoleId { get; set; }

        /// <summary>
        /// 模块|菜单集合
        /// </summary>
        public IList<RolesOwnedModules> RolesOwnedModules { get; set; } = new List<RolesOwnedModules>();

        /// <summary>
        /// RoleClaims
        /// </summary>
        public IList<RoleClaims> RoleClaims { get; set; } = new List<RoleClaims>();
    }

    /// <summary>
    /// 模块|菜单
    /// </summary>
    public class RolesOwnedModules
    {
        /// <summary>
        /// 模块|菜单标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "菜单标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "菜单标识不正确")]
        public string ModuleId { get; set; }
    }

    /// <summary>
    /// RoleClaims
    /// </summary>
    public class RoleClaims
    {
        /// <summary>
        /// ClaimValue
        /// </summary>
        [Required]
        [StringLength(32, ErrorMessage = "ClaimValue的长度为 2 到 32 个字符", MinimumLength = 2)]
        public string ClaimValue { get; set; }
    }
}
