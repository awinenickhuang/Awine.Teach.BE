using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 营销渠道
    /// </summary>
    public class MarketingChannel : Entity
    {
        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Describe
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
