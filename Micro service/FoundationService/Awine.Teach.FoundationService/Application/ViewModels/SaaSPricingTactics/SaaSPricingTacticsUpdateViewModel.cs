using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// SaaS定价策略
    /// </summary>
    public class SaaSPricingTacticsUpdateViewModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }

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
    }
}
