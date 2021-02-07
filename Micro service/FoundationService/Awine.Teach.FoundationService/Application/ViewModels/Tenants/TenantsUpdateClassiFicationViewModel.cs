using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 更新 -> 租户类型 -> 视图模型
    /// </summary>
    public class TenantsUpdateClassiFicationViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "主键标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "主键标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 租户类型 1-免费 2-试用 3-付费（VIP）
        /// </summary>
        [Required(ErrorMessage = "租户类型必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "租户类型只能输入正整数")]
        public int ClassiFication { get; set; }
    }
}
