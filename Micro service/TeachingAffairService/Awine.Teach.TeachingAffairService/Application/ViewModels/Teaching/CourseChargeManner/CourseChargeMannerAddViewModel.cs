using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 课程收费方式 -> 添加 -> 视图模型
    /// </summary>
    public class CourseChargeMannerAddViewModel
    {
        /// <summary>
        /// 课程标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "课程标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "课程标识不正确")]
        public string CourseId { get; set; }

        /// <summary>
        /// 收费方式 1-按课时收费 2-按月收费
        /// </summary>
        [Required(ErrorMessage = "收费方式必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "收费方式只能输入正整数")]
        public int ChargeManner { get; set; }

        /// <summary>
        /// 课程总价（元）
        /// </summary>
        [Required(ErrorMessage = "课程总价必填")]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 课程授课时长 单位（收费方按课时时为课时，按月时为月）
        /// </summary>
        [Required(ErrorMessage = "课程授课时长必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "课程授课时长只能输入正整数")]
        public int CourseDuration { get; set; }

        /// <summary>
        /// 收费标准 - 单价 - 元/课时
        /// </summary>
        [Required(ErrorMessage = "收费标准必填")]
        public decimal ChargeUnitPriceClassHour { get; set; }

        /// <summary>
        /// 收费标准 - 单价 - 元/月
        /// </summary>
        [Required(ErrorMessage = "收费标准必填")]
        public decimal ChargeUnitPriceMonth { get; set; }
    }
}
