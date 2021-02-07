using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 补课学生 -> 从班级中移除 -> 视图模型
    /// </summary>
    public class MakeupMissedLessonStudentRemoveViewModel
    {
        /// <summary>
        /// 补课班级标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "补课班级标识标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "补课班级标识识不正确")]
        public string MakeupMissedLessonId { get; set; }

        /// <summary>
        /// 学生信息
        /// </summary>
        public IList<MksStudent> MksStudents { get; set; } = new List<MksStudent>();
    }

    /// <summary>
    /// 学生信息
    /// </summary>
    public class MksStudent
    {
        /// <summary>
        /// 补课学生标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "补课学生标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "补课学生标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 考勤标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "考勤标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "考勤标识不正确")]
        public string AttendanceId { get; set; }
    }
}
