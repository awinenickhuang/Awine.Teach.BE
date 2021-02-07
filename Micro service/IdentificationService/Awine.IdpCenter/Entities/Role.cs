using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.Entities
{
    /// <summary>
    /// 企业用户角色
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 每当用户被持久化到存储库时必须改变的随机值，如果两个并发更新同时出现，则它们必须具有相同的戳记，或者其中一个应该被丢弃
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 正规化名称
        /// </summary>
        public string NormalizedName { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
