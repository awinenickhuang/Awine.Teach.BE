using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 折线图堆叠图
    /// </summary>
    public class StackedLineChartViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public StackedLineChartLegend Legend { get; set; } = new StackedLineChartLegend();

        /// <summary>
        /// 
        /// </summary>
        public StackedLineChartxAxis XAxis { get; set; } = new StackedLineChartxAxis();

        /// <summary>
        /// 
        /// </summary>
        public List<StackedLineChartSeries> Series { get; set; } = new List<StackedLineChartSeries>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class StackedLineChartLegend
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> Data { get; set; } = new List<string>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class StackedLineChartxAxis
    {
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; } = "category";

        /// <summary>
        /// 
        /// </summary>
        public bool BoundaryGap { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public List<string> Data { get; set; } = new List<string>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class StackedLineChartSeries
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; } = "line";

        /// <summary>
        /// 
        /// </summary>
        public string Stack { get; set; } = "总数";

        /// <summary>
        /// 
        /// </summary>
        public List<int> Data { get; set; } = new List<int>();
    }
}
