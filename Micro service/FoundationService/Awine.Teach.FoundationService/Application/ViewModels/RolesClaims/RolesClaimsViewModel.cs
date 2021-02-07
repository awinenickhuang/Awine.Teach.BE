using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 角色声明信息 -> 视图模型
    /// </summary>
    public class RolesClaimsViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string ClaimValue { get; set; }
    }
}
