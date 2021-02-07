using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 保存 -> 角色数据权限 -> 视图模型
    /// </summary>
    public class RolesOwnedTenantSaveViewModel
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "角色标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "角色标识不正确")]
        public string RoleId { get; set; }

        /// <summary>
        /// 平台租户信息
        /// </summary>
        public List<RolesOwnedTenantAddViewModel> RolesOwnedTenantAddViewModel { get; set; } = new List<RolesOwnedTenantAddViewModel>();
    }

    /// <summary>
    /// 平台租户信息
    /// </summary>
    public class RolesOwnedTenantAddViewModel
    {
        /// <summary>
        /// 租户标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "租户标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "租户标识不正确")]
        public string TenantId { get; set; }
    }
}
