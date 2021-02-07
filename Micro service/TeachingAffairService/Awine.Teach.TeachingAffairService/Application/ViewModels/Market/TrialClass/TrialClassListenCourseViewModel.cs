using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 创建试听 -> 个对一 -> 视图模型
    /// </summary>
    public class TrialClassListenCourseViewModel
    {
        /// <summary>
        /// 学生标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "课程标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "课程标识不正确")]
        public string StudentId { get; set; }

        /// <summary>
        /// 学生类型 1-意向学生 2-正式学生
        /// </summary>
        [Required(ErrorMessage = "学生类型必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "学生类型只能输入正整数")]
        public int StudentCategory { get; set; }

        /// <summary>
        /// 课程标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "课程标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "课程标识不正确")]
        public string CourseId { get; set; }

        /// <summary>
        /// 试听课老师标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "试听课老师标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "试听课老师标识不正确")]
        public string TeacherId { get; set; }

        /// <summary>
        /// 试听课老师姓名
        /// </summary>
        [Required(ErrorMessage = "试听课老师姓名必填"), MaxLength(6, ErrorMessage = "试听课老师姓名太长啦"), MinLength(2, ErrorMessage = "试听课老师姓名太短啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "试听课老师姓名必须是中文")]
        public string TeacherName { get; set; }

        /// <summary>
        /// 试听课教室标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "试听课教室标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "试听课教室标识不正确")]
        public string ClassRoomId { get; set; }

        /// <summary>
        /// 试听课安排时间
        /// </summary>
        [Required(ErrorMessage = "试听课安排时间必填")]
        [DataType(DataType.Date)]
        public DateTime CourseDates { get; set; }

        /// <summary>
        /// 上课开始时间
        /// </summary>
        [Required(ErrorMessage = "上课开始时间必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "上课开始时间只能输入正整数")]
        public int StartHours { get; set; }

        /// <summary>
        /// 上课开始分钟
        /// </summary>
        [Required(ErrorMessage = "上课开始分钟必填")]
        [RegularExpression(@"^\+?(0|[1-9][0-9]*)$", ErrorMessage = "上课开始分钟只能输入整数")]
        public int StartMinutes { get; set; }

        /// <summary>
        /// 上课结束时间
        /// </summary>
        [Required(ErrorMessage = "上课结束时间必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "上课结束时间只能输入正整数")]
        public int EndHours { get; set; }

        /// <summary>
        /// 上课结束分钟 
        /// </summary>
        [Required(ErrorMessage = "上课结束分钟必填")]
        [RegularExpression(@"^\+?(0|[1-9][0-9]*)$", ErrorMessage = "上课结束分钟只能输入整数")]
        public int EndMinutes { get; set; }
    }
}
