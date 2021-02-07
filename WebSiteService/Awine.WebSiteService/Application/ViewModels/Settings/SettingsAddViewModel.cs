using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.WebSite.Applicaton.ViewModels
{
    public class SettingsAddViewModel
    {
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
        public int Visible { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
