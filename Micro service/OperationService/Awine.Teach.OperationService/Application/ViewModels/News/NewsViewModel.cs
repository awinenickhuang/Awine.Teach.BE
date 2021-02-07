using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.OperationService.Application.ViewModels
{
    /// <summary>
    /// 行业资讯 -> 视图模型
    /// </summary>
    public class NewsViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 内容来源
        /// </summary>
        public string ContentSource { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 建创时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
