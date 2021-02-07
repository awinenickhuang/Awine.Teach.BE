using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 系统模块 -> 树型数据结构 -> 视图模型
    /// </summary>
    public class ModulesTreeViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否选中 -> XMSelect
        /// </summary>
        public bool Selected { get; set; } = false;

        /// <summary>
        /// 父级标识
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 模块描述
        /// </summary>
        public string DescText { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 转跳地址
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string ModuleIcon { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 叶子节点
        /// </summary>
        public IList<ModulesTreeViewModel> Children { get; set; }
    }
}
