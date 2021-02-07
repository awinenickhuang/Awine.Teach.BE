using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.OperationService.Application.ViewModels
{
    /// <summary>
    /// 行业资讯 -> 添加 -> 视图模型
    /// </summary>
    public class NewsAddViewModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "标题必填")]
        [StringLength(64, ErrorMessage = "标题长度为2-64个字符", MinimumLength = 2)]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "内容必填")]
        [StringLength(1024, ErrorMessage = "内容长度为2-1024个字符", MinimumLength = 2)]
        public string Content { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "作者必填")]
        [StringLength(12, ErrorMessage = "作者长度为2-12个字符", MinimumLength = 2)]
        public string Author { get; set; }

        /// <summary>
        /// 内容来源
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "内容来源必填")]
        [StringLength(64, ErrorMessage = "内容来源长度为2-64个字符", MinimumLength = 2)]
        public string ContentSource { get; set; }
    }
}
