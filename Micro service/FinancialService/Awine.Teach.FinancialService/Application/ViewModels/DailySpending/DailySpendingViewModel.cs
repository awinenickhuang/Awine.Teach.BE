using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FinancialService.Application.ViewModels
{
    /// <summary>
    /// 日常支出 -> 视图模型
    /// </summary>
    public class DailySpendingViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

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
        /// 建创时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
