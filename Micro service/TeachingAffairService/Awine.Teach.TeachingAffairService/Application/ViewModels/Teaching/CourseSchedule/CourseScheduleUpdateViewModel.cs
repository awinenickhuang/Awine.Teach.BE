using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 班级 -> 排课信息 -> 更新 -> 视图模型
    /// </summary>
    public class CourseScheduleUpdateViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "唯一标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "唯一标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 班级标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "班级标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "班级标识不正确")]
        public string ClassId { get; set; }

        /// <summary>
        /// 上课日期
        /// </summary>
        [Required(ErrorMessage = "上课日期必填")]
        [DataType(DataType.Date)]
        public DateTime CourseDates { get; set; }

        /// <summary>
        /// 开始几点
        /// </summary>
        [Required(ErrorMessage = "课节开始时间必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "课节开始时间只能输入正整数")]
        public int StartHours { get; set; } = 0;

        /// <summary>
        /// 开始几分
        /// </summary>
        [Required(ErrorMessage = "课节开始分钟必填")]
        [RegularExpression(@"^\+?[0-9][0-9]*$", ErrorMessage = "课节开始分钟只能输入整数")]
        public int StartMinutes { get; set; } = 0;

        /// <summary>
        /// 结束几点
        /// </summary>
        [Required(ErrorMessage = "课节结束时间必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "课节结束时间只能输入正整数")]
        public int EndHours { get; set; } = 0;

        /// <summary>
        /// 结几分
        /// </summary>
        [Required(ErrorMessage = "课节结束分钟必填")]
        [RegularExpression(@"^\+?[0-9][0-9]*$", ErrorMessage = "课节结束分钟只能输入整数")]
        public int EndMinutes { get; set; } = 0;

        /// <summary>
        /// 教师标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "教师标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "教师标识不正确")]
        public string TeacherId { get; set; }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [Required(ErrorMessage = "教师姓名必填"), MaxLength(6, ErrorMessage = "教师姓名太长啦"), MinLength(2, ErrorMessage = "教师姓名太短啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "教师姓名必须是中文")]
        public string TeacherName { get; set; }

        /// <summary>
        /// 课程标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "课程标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "课程标识不正确")]
        public string CourseId { get; set; }

        /// <summary>
        /// 教室标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "教室标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "教室标识不正确")]
        public string ClassRoomId { get; set; }
    }
}
