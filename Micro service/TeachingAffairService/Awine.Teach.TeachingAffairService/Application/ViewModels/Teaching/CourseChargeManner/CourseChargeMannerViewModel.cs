using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 课程收费方式 -> 视图模型
    /// </summary>
    public class CourseChargeMannerViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 课程标识
        /// </summary>
        public string CourseId { get; set; }

        /// <summary>
        /// 收费方式 1-按课时收费 2-按月收费
        /// </summary>
        public int ChargeManner { get; set; }

        /// <summary>
        /// 课程总价（元）
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 课程授课时长 单位（收费方按课时时为课时，按月时为月）
        /// </summary>
        public int CourseDuration { get; set; }

        /// <summary>
        /// 收费标准 - 单价 - 元/课时
        /// </summary>
        public decimal ChargeUnitPriceClassHour { get; set; }

        /// <summary>
        /// 收费标准 - 单价 - 元/月
        /// </summary>
        public decimal ChargeUnitPriceMonth { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
