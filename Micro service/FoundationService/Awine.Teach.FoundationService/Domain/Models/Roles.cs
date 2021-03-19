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
        /// 角色类型 1-平台超管 2-代理超管 3-租户超管 9-普通角色
        /// </summary>
        public int Identifying { get; set; } = 9;

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
