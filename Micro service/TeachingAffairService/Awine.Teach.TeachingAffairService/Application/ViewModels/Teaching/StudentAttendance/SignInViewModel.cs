using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 课节点名 -> 视图模型
    /// </summary>
    public class SignInViewModel
    {
        /// <summary>
        /// 课节标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "课节标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "课节标识不正确")]
        public string CourseScheduleId { get; set; }

        /// <summary>
        /// 试听学生
        /// </summary>
        public IList<ListenStudent> ListenStudents { get; set; } = new List<ListenStudent>();

        /// <summary>
        /// 正式学生
        /// </summary>
        public IList<OfficialStudent> OfficialStudents { get; set; } = new List<OfficialStudent>();
    }

    /// <summary>
    /// 试听学生
    /// </summary>
    public class ListenStudent
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "唯一标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "唯一标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 试听状态 1-已创建 2-已到课 3-已失效
        /// </summary>
        [Required(ErrorMessage = "试听状态必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "试听状态只能输入正整数")]
        public int ListeningState { get; set; }
    }

    /// <summary>
    /// 正式学生
    /// </summary>
    public class OfficialStudent
    {
        /// <summary>
        /// 学生标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "学生标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "学生标识不正确")]
        public string StudentId { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        [Required(ErrorMessage = "学生姓名必填"), MaxLength(6, ErrorMessage = "学生姓名太长啦"), MinLength(2, ErrorMessage = "学生姓名太短啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "学生姓名必须是中文")]
        public string StudentName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "性别只能输入正整数")]
        public int Gender { get; set; }

        /// <summary>
        /// 报读课程标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "学生标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "学生标识不正确")]
        public string StudentCourseItemId { get; set; }

        /// <summary>
        /// 出勤状态 1-出勤 2-缺勤 3-请假
        /// </summary>
        [Required(ErrorMessage = "出勤状态必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "出勤状态只能输入正整数")]
        public int AttendanceState { get; set; }

        /// <summary>
        /// 课消数量
        /// </summary>
        [Required(ErrorMessage = "课消数量必填")]
        [RegularExpression(@"^\+?[0-9][0-9]*$", ErrorMessage = "课消数量只能输入正整数")]
        public int ConsumedQuantity { get; set; }
    }
}
