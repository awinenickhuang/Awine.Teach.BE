using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 饼状图 -> 视图模型
    /// </summary>
    public class PieChartViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<LegendData> LegendData { get; set; } = new List<LegendData>();

        /// <summary>
        /// 
        /// </summary>
        public List<PieChartSeriesData> SeriesData { get; set; } = new List<PieChartSeriesData>();
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
    public class LegendData
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}
