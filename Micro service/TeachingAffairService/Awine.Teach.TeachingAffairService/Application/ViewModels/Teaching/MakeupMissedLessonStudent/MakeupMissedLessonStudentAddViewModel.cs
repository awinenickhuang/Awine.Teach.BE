using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 补课学生 -> 添加到班级 -> 视图模型
    /// </summary>
    public class MakeupMissedLessonStudentAddViewModel
    {
        /// <summary>
        /// 补课班级标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "补课班级标识标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "补课班级标识识不正确")]
        public string MakeupMissedLessonId { get; set; }

        /// <summary>
        /// 考勤信息
        /// </summary>
        public IList<MksAttendance> MksAttendances { get; set; } = new List<MksAttendance>();
    }

    /// <summary>
    /// 考勤信息
    /// </summary>
    public class MksAttendance
    {
        /// <summary>
        /// 考勤标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "考勤标识标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "考勤标识标识不正确")]
        public string AttendanceId { get; set; }
    }
}
