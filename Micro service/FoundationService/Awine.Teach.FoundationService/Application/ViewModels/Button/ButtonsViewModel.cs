using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 按钮 -> 视图模型
    /// </summary>
    public class ButtonsViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; }

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
        public string ButtonIcon { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string AccessCode { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
