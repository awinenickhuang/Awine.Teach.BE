using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 班级 ->添加 -> 视图模型
    /// </summary>
    public class ClassesAddViewModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "名称必填")]
        [StringLength(32, ErrorMessage = "名称长度为2-32个字符", MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 课程标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "课程标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "课程标识不正确")]
        public string CourseId { get; set; }

        /// <summary>
        /// 班级容量（人数限制）
        /// </summary>
        [Required(ErrorMessage = "班级容量必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "班级容量只能输入正整数")]
        public int ClassSize { get; set; }

        /// <summary>
        /// 老师标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "老师标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "老师标识不正确")]
        public string TeacherId { get; set; }

        /// <summary>
        /// 老师姓名
        /// </summary>
        [Required(ErrorMessage = "姓名必填"), MaxLength(6, ErrorMessage = "姓名太长啦"), MinLength(2, ErrorMessage = "姓名太短啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "姓名必须是中文")]
        public string TeacherName { get; set; }

        /// <summary>
        /// 开班日期
        /// </summary>
        [Required(ErrorMessage = "开班日期必填")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 招生状态 1-开放招生 2-暂停招生 3-授课结束
        /// </summary>
        [Required(ErrorMessage = "招生状态必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "招生状态只能输入正整数")]
        public int RecruitStatus { get; set; }

        /// <summary>
        /// 上课教室标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "上课教室标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "上课教室标识不正确")]
        public string ClassRoomId { get; set; }

        /// <summary>
        /// 班级类型 1-one to many 2-one to one
        /// </summary>
        [Required(ErrorMessage = "班级类型必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "班级类型只能输入正整数")]
        public int TypeOfClass { get; set; }
    }
}
