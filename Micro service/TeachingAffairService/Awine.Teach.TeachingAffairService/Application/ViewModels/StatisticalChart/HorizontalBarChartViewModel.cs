using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 横向条形柱状图
    /// </summary>
    public class HorizontalBarChartViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<HorizontalBarChartYAxis> HorizontalBarChartYAxis { get; set; } = new List<HorizontalBarChartYAxis>();

        /// <summary>
        /// 
        /// </summary>
        public List<HorizontalBarChartSeries> HorizontalBarChartSeries { get; set; } = new List<HorizontalBarChartSeries>();
    }

    /// <summary>
    /// Y轴数据
    /// </summary>
    public class HorizontalBarChartYAxis
    {
        /// <summary>
        /// 
        /// </summary>
        public int TrackingState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HorizontalBarChartSeries
    {
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }
    }
}
