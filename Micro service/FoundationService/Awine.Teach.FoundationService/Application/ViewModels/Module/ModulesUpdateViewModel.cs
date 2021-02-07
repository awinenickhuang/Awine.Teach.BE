using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 系统模块 -> 更新 -> 视图模型
    /// </summary>
    public class ModulesUpdateViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "主键标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "主键标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "模块名称必填")]
        [StringLength(32, ErrorMessage = "模块名称长度为2-32个字符", MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 父级标识
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 模块描述
        /// </summary>
        [StringLength(50, ErrorMessage = "模块描述长度为0-50个字符", MinimumLength = 0)]
        public string DescText { get; set; }

        /// <summary>
        /// 转跳地址
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "转跳地址必填")]
        [StringLength(64, ErrorMessage = "转跳地址长度为1-64个字符", MinimumLength = 1)]
        public string RedirectUri { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string ModuleIcon { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required(ErrorMessage = "显示顺序必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "显示顺序只能输入正整数")]
        public int DisplayOrder { get; set; }
    }
}
