using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.WebSite.Applicaton.ViewModels
{
    public class ForumTreeViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 是否选中 -> XMSelect
        /// </summary>
        public bool Selected { get; set; } = false;

        /// <summary>
        /// 描述文字
        /// </summary>
        public string DescribeText { get; set; }

        /// <summary>
        /// 父级标识
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 显示属性 1-头部导航菜单 2-底部导航菜单
        /// </summary>
        public int DisplayAttribute { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 内容展示类型 1-列表 2-单页
        /// </summary>
        public int ContentAttribute { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string RedirectAddress { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 叶子节点
        /// </summary>
        public IList<ForumTreeViewModel> Children { get; set; }
    }
}
