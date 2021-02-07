using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 按钮 -> 公共数据
    /// </summary>
    public class Buttons : Entity
    {
        /// <summary>
        /// 模块标识
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 按钮图标
        /// </summary>
        public string ButtonIcon { get; set; } = string.Empty;

        /// <summary>
        /// 权限编码
        /// </summary>
        public string AccessCode { get; set; } = string.Empty;

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
