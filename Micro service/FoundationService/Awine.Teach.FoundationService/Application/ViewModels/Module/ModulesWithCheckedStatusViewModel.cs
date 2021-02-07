using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 系统模块 -> 带选中状态 -> 视图模型
    /// </summary>
    public class ModulesWithCheckedStatusViewModel
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
        /// 父级标识
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; } = false;
    }
}
