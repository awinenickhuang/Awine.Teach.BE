using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.Model
{
    /// <summary>
    /// 生源统计 -> 绘图 -> 基本折线图 -> 视图模型
    /// </summary>
    public class BasicLineChartViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> XAxisData { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public List<long> SeriesData { get; set; } = new List<long>();
    }
}
