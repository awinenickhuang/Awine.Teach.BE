using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.Model
{
    /// <summary>
    /// 堆叠面积图
    /// </summary>
    public class StackedAreaChartViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<StackedAreaChartLegend> Legend { get; set; } = new List<StackedAreaChartLegend>();

        /// <summary>
        /// 
        /// </summary>
        public List<StackedAreaChartxAxis> XAxis { get; set; } = new List<StackedAreaChartxAxis>();

        /// <summary>
        /// 
        /// </summary>
        public List<StackedAreaChartSeries> Series { get; set; } = new List<StackedAreaChartSeries>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class StackedAreaChartLegend
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StackedAreaChartxAxis
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
    public class StackedAreaChartSeries
    {
        /// <summary>
        /// 为方便取数据而添加，不和 EChart 一致，故在返回的时候忽略
        /// </summary>
        [JsonIgnore]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; } = "line";

        /// <summary>
        /// 
        /// </summary>
        public string Stack { get; set; } = "数量";

        /// <summary>
        /// 
        /// </summary>
        public StackedAreaChartSeriesItemStyle ItemStyle { get; set; } = new StackedAreaChartSeriesItemStyle();

        /// <summary>
        /// 
        /// </summary>
        public List<int> Data { get; set; } = new List<int>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class StackedAreaChartSeriesItemStyle
    {
        /// <summary>
        /// 
        /// </summary>
        public StackedAreaChartSeriesItemStyleNormal Normal { get; set; } = new StackedAreaChartSeriesItemStyleNormal();
    }

    /// <summary>
    /// 
    /// </summary>
    public class StackedAreaChartSeriesItemStyleNormal
    {
        /// <summary>
        /// 
        /// </summary>
        public StackedAreaChartSeriesItemStyleNormalAreaStyle AreaStyle { get; set; } = new StackedAreaChartSeriesItemStyleNormalAreaStyle();
    }

    /// <summary>
    /// 
    /// </summary>
    public class StackedAreaChartSeriesItemStyleNormalAreaStyle
    {
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; } = "default";
    }
}
