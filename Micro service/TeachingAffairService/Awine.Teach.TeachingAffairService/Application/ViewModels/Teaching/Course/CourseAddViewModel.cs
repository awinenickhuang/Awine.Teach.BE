using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 课程 -> 添加 -> 视图模型
    /// </summary>
    public class CourseAddViewModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "名称必填")]
        [StringLength(32, ErrorMessage = "名称长度为2-32个字符", MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 课程名师
        /// </summary>

        [Required(AllowEmptyStrings = false, ErrorMessage = "课程名师标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "课程名师标识不正确")]
        public string TeacherId { get; set; }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [Required(ErrorMessage = "教师姓名必填"), MaxLength(6, ErrorMessage = "教师姓名太长啦"), MinLength(2, ErrorMessage = "教师姓名太短啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "教师姓名必须是中文")]
        public string TeacherName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required(ErrorMessage = "显示顺序必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "显示顺序只能输入正整数")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 课程介绍
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "课程介绍必填")]
        [StringLength(5000, ErrorMessage = "课程介绍长度为1-5000个字符", MinimumLength = 1)]
        public string CourseIntroduction { get; set; }

        /// <summary>
        /// 课程状态 1-启用 2-停用
        /// </summary>
        [Required(ErrorMessage = "课程状态必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "课程状态只能输入正整数")]
        public int EnabledStatus { get; set; }
    }
}
