using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.AspNetCore.Model
{
    /// <summary>
    /// 曲线图 -> 视图模型
    /// </summary>
    public class CurveChartViewModel
    {
        /// <summary>
        /// X
        /// </summary>
        public string ChartXAxis { get; set; }

        /// <summary>
        /// Y
        /// </summary>
        public string ChartYAxis { get; set; }

        /// <summary>
        /// Series
        /// </summary>
        public List<Series> Series { get; set; } = new List<Series>();
    }

    /// <summary>
    /// 图表数据定义
    /// </summary>
    public class Series
    {
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; } = "line";

        /// <summary>
        /// 
        /// </summary>
        public List<int> Data { get; set; } = new List<int>();
    }
}