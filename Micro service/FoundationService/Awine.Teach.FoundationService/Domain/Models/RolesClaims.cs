using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 角色声明信息
    /// </summary>
    public class RolesClaims
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

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
