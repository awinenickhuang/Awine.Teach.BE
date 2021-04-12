using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 角色 -> 添加 -> 视图模型
    /// </summary>
    public class RolesAddViewModel
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [StringLength(32, ErrorMessage = "{0} 角色名称的长度为 {2} 到 {32}个字符", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
