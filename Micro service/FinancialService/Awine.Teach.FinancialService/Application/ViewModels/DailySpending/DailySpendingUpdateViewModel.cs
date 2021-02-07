using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FinancialService.Application.ViewModels
{
    /// <summary>
    /// 日常支出 ->更新 -> 视图模型
    /// </summary>
    public class DailySpendingUpdateViewModel
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
    }
}
