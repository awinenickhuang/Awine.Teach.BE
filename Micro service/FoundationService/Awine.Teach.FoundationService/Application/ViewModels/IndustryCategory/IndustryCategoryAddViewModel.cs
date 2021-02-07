using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 行业 -> 添加 -> 视图模型
    /// </summary>
    public class IndustryCategoryAddViewModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "名称必填")]
        [StringLength(20, ErrorMessage = "名称长度为2-20个字符", MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(100, ErrorMessage = "描述长度为2-20个字符", MinimumLength = 2)]
        public string DescText { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required(ErrorMessage = "显示顺序必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "显示顺序只能输入正整数")]
        public int DisplayOrder { get; set; }
    }
}
