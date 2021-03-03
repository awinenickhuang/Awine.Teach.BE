using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.Model
{
    /// <summary>
    /// 饼状图 -> 视图模型
    /// </summary>
    public class PieChartViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> LegendData { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public List<PieChartSeriesData> SeriesData { get; set; } = new List<PieChartSeriesData>();

        /// <summary>
        /// 
        /// </summary>
        public List<PieChartSeriesDecimalData> SeriesDecimalData { get; set; } = new List<PieChartSeriesDecimalData>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class PieChartSeriesDecimalData
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PieChartSeriesData
    {
        /// <summary>
        /// 
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PieChartLegendData
    {
        /// <summary>
        /// 
        /// </summary>
        public string Orient { get; set; } = "horizontal";

        /// <summary>
        /// 
        /// </summary>
        public string Left { get; set; } = "left";

        /// <summary>
        /// 
        /// </summary>
        public List<string> Data { get; set; } = new List<string>();
    }
}
