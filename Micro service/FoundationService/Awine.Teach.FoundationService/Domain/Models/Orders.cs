using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 订单数据
    /// </summary>
    public class Orders
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 租户ID
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 购买年数
        /// </summary>
        public int NumberOfYears { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayTheAmount { get; set; }

        /// <summary>
        /// 业绩归属人ID
        /// </summary>
        public string PerformanceOwnerId { get; set; }

        /// <summary>
        /// 业绩归属人ID
        /// </summary>
        public string PerformanceOwner { get; set; }

        /// <summary>
        /// 业绩归属租户ID
        /// </summary>
        public string PerformanceTenantId { get; set; }

        /// <summary>
        /// 业绩归属租户
        /// </summary>
        public string PerformanceTenant { get; set; }

        /// <summary>
        /// 交易类别 1-新购 2-续费
        /// </summary>
        public int TradeCategories { get; set; }

        /// <summary>
        /// SaaS版本ID
        /// </summary>
        public string SaaSVersionId { get; set; }

        /// <summary>
        /// SaaS版本
        /// </summary>
        public string SaaSVersionName { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
