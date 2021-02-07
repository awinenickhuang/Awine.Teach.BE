using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 角色 -> 添加 -> 视图模型
    /// </summary>
    public class RolesAddViewModel
    {
        /// <summary>
        /// 租户标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "租户标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "租户标识不正确")]
        public string TenantId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [StringLength(32, ErrorMessage = "{0} 角色名称的长度为 {2} 到 {32}个字符", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
