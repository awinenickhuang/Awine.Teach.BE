using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 系统模块
    /// </summary>
    public class Modules : Entity
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级标识
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 模块描述
        /// </summary>
        public string DescText { get; set; } = string.Empty;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 转跳地址
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string ModuleIcon { get; set; } = string.Empty;

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
