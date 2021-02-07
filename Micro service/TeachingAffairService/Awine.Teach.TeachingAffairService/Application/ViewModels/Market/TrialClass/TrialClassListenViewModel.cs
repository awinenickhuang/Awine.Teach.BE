using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 创建试听 -> 跟班试听 -> 视图模型
    /// </summary>
    public class TrialClassListenViewModel
    {
        /// <summary>
        /// 学生标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "学生标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "学生标识不正确")]
        public string StudentId { get; set; }

        /// <summary>
        /// 学生类型 1-意向学生 2-正式学生
        /// </summary>
        [Required(ErrorMessage = "学生类型必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "学生类型只能输入正整数")]
        public int StudentCategory { get; set; }

        /// <summary>
        /// 课节标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "课节标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "课节标识不正确")]
        public string CourseScheduleId { get; set; }
    }
}
