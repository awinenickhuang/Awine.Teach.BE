using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.WebSite.Domain
{
    public class Settings
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 标题
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string ItemValue { get; set; }

        /// <summary>
        /// 1-展示底部 2-不限位置
        /// </summary>
        public int Visible { get; set; } = 1;

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; } = 1;
    }
}
