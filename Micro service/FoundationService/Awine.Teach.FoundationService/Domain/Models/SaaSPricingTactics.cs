using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// SaaS版本定价策略
    /// </summary>
    public class SaaSPricingTactics
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 版本ID
        /// </summary>
        public string SaaSVersionId { get; set; }

        /// <summary>
        /// 年数
        /// </summary>
        public int NumberOfYears { get; set; }

        /// <summary>
        /// 收费标准
        /// </summary>
        public decimal ChargeRates { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
