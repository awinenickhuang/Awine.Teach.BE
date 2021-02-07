using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 生源情况统计 -> 视图模型
    /// </summary>
    public class ConsultRecordStatisticalViewModel
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalAmount { get; set; }

        /// <summary>
        /// 待跟进
        /// </summary>
        public int TofollowupAmount { get; set; }

        /// <summary>
        /// 跟进中
        /// </summary>
        public int InthefollowupAmount { get; set; }

        /// <summary>
        /// 已成交
        /// </summary>
        public int TradedAmount { get; set; }
    }
}
