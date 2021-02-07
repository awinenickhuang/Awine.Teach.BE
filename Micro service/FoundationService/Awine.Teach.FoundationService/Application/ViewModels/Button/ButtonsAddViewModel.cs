using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 按钮 -> 添加 -> 视图模型
    /// </summary>
    public class ButtonsAddViewModel
    {
        /// <summary>
        /// 模块标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "模块标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "模块标识不正确")]
        public string ModuleId { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "按钮名称必填")]
        [StringLength(32, ErrorMessage = "按钮名称长度为6-32个字符", MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 按钮图标
        /// </summary>
        public string ButtonIcon { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "权限编码必填")]
        [StringLength(32, ErrorMessage = "权限编码长度为2-32个字符", MinimumLength = 2)]
        public string AccessCode { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required(ErrorMessage = "显示顺序必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "显示顺序只能输入正整数")]
        public int DisplayOrder { get; set; }
    }
}
