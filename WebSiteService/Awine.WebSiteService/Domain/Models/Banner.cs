using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.WebSite.Domain
{
    /// <summary>
    /// 横幅图片
    /// </summary>
    public class Banner
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
        /// 标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; } = 1;

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 数据类型 1-网站Banner 2-栏目Banner
        /// </summary>
        public int Category { get; set; } = 1;

        /// <summary>
        /// 栏目标识
        /// </summary>
        public string ForumId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
