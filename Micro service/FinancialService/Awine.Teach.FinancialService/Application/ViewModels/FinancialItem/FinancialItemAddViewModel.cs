using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FinancialService.Application.ViewModels
{
    /// <summary>
    /// 财务收支项目 -> 添加 -> 视图模型
    /// </summary>
    public class FinancialItemAddViewModel
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "项目名称必填")]
        [StringLength(64, ErrorMessage = "项目名称长度为1-64个字符", MinimumLength = 1)]
        public string Name { get; set; }

        /// <summary>
        /// 启用状态 1-启用 2-停用
        /// </summary>
        [Required(ErrorMessage = "启用状态必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "启用状态只能输入正整数")]
        public int Status { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required(ErrorMessage = "显示顺序必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "显示顺序只能输入正整数")]
        public int DisplayOrder { get; set; }
    }
}
