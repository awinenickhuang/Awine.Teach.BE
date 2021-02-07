using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 排课数据 -> 按天统计 -> 视图模型
    /// </summary>
    public class CourseScheduleStatisticalViewModel
    {
        /// <summary>
        /// 上课日期
        /// </summary>
        public string ScheduleDate { get; set; }

        /// <summary>
        /// 当天课节数
        /// </summary>
        public int ScheduleCount { get; set; }
    }
}
