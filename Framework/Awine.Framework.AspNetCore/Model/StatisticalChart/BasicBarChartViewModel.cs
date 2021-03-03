using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.Model
{
    /// <summary>
    /// 基础柱状图
    /// </summary>
    public class BasicBarChartViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> XAxis { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public List<decimal> SeriesDecimalData { get; set; } = new List<decimal>();

        /// <summary>
        /// 
        /// </summary>
        public List<long> SeriesLongData { get; set; } = new List<long>();
    }
}
