using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.WebSite.Applicaton.ViewModels
{
    /// <summary>
    /// 文章管理 -> 更新 -> 视图模型
    /// </summary>
    public class ArticleUpdateViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "主键标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "主键标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "标题必填")]
        [StringLength(50, ErrorMessage = "标题长度为1-50个字符", MinimumLength = 1)]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "内容必填")]
        [StringLength(50000, ErrorMessage = "内容长度为1-50000个字符", MinimumLength = 1)]
        public string Content { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "作者必填")]
        [StringLength(16, ErrorMessage = "作者长度为1-16个字符", MinimumLength = 1)]
        public string Author { get; set; }

        /// <summary>
        /// 内容来源
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "内容来源必填")]
        [StringLength(32, ErrorMessage = "内容来源长度为1-32个字符", MinimumLength = 1)]
        public string ContentSource { get; set; }

        /// <summary>
        /// 栏目标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "栏目标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "栏目标识不正确")]
        public string ForumId { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverPicture { get; set; }
    }
}
