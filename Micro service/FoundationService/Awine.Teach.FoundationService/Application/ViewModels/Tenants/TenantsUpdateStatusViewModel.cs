using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 更新 -> 租户状态 -> 视图模型
    /// </summary>
    public class TenantsUpdateStatusViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "主键标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "主键标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 租户状态 1-正常 2-锁定（异常）3-锁定（过期）
        /// </summary>
        [Required(ErrorMessage = "租户状态必填")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "租户状态只能输入整数")]
        public int Status { get; set; }
    }
}
