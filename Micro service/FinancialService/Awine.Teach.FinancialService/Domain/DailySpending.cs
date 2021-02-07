using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FinancialService.Domain
{
    /// <summary>
    /// 日常支出
    /// </summary>
    public class DailySpending : Entity
    {
        /// <summary>
        /// 支出项目
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 支出金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 收支项目标识
        /// </summary>
        public string FinancialItemId { get; set; }

        /// <summary>
        /// 收支项目名称
        /// </summary>
        public string FinancialItemName { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
