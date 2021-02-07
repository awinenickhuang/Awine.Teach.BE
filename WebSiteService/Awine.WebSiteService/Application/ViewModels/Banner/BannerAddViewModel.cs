using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.WebSite.Applicaton.ViewModels
{
    /// <summary>
    /// 横幅图片 -> 添加 -> 视图模型
    /// </summary>
    public class BannerAddViewModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "标题必填")]
        [StringLength(32, ErrorMessage = "标题长度为1-32个字符", MinimumLength = 1)]
        public string Title { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "标题必填")]
        [StringLength(32, ErrorMessage = "标题长度为1-32个字符", MinimumLength = 1)]
        public string SubTitle { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required(ErrorMessage = "显示顺序必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "显示顺序只能输入正整数")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "图片地址必填")]
        [StringLength(128, ErrorMessage = "图片地址长度为1-128个字符", MinimumLength = 1)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 数据类型 1-网站Banner 2-栏目Banner
        /// </summary>
        [Required(ErrorMessage = "数据类型必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "数据类型只能输入正整数")]
        public int Category { get; set; }

        /// <summary>
        /// 栏目标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "栏目标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "栏目标识不正确")]
        public string ForumId { get; set; }
    }
}
