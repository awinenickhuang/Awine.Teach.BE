using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.WebSite.Domain
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public class Article
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

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
        public int ViewCount { get; set; } = 0;

        /// <summary>
        /// 栏目标识
        /// </summary>
        public string ForumId { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverPicture { get; set; } = "";

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
