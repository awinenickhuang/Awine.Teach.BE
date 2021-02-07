using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.WebSite.Applicaton.ViewModels
{
    /// <summary>
    /// 版块管理 -> 添加 -> 视图模型
    /// </summary>
    public class ForumAddViewModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "名称必填")]
        [StringLength(32, ErrorMessage = "名称长度为1-32个字符", MinimumLength = 1)]
        public string Name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "英文名称必填")]
        [StringLength(32, ErrorMessage = "英文名称长度为1-32个字符", MinimumLength = 1)]
        public string EnglishName { get; set; }

        /// <summary>
        /// 描述文字
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "描述文字必填")]
        [StringLength(32, ErrorMessage = "描述文字长度为1-32个字符", MinimumLength = 1)]
        public string DescribeText { get; set; }

        /// <summary>
        /// 父级标识
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 显示属性 1-头部导航菜单 2-底部导航菜单
        /// </summary>
        [Required(ErrorMessage = "显示属性必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "显示属性只能输入正整数")]
        public int DisplayAttribute { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required(ErrorMessage = "显示顺序必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "显示顺序只能输入正整数")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 内容展示类型 1-列表 2-单页
        /// </summary>
        [Required(ErrorMessage = "内容展示类型必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "内容展示类型只能输入正整数")]
        public int ContentAttribute { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "名称必填")]
        [StringLength(128, ErrorMessage = "名称长度为1-128", MinimumLength = 1)]
        public string RedirectAddress { get; set; }
    }
}
