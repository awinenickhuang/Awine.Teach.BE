using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FinancialService.Application.ViewModels
{
    /// <summary>
    /// 财务收支项目 -> 更新状态 -> 视图模型
    /// </summary>
    public class FinancialItemUpdateStatusViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "主键标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "主键标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 状态 1-启用 2-禁用
        /// </summary>
        [Required(ErrorMessage = "状态必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "状态只能输入正整数")]
        public int Status { get; set; }
    }
}
