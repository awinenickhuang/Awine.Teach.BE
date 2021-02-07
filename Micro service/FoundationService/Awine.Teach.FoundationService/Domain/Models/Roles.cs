using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 系统角色
    /// </summary>
    public class Roles : Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 同步标记
        /// </summary>
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 标准化名称
        /// </summary>
        public string NormalizedName { get; set; } = string.Empty;

        /// <summary>
        /// 是否超级管理员角色
        /// </summary>
        public bool IsSuperRole { get; set; } = false;

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 租户信息
        /// </summary>
        public Tenants PlatformTenant { get; set; }
    }
}
