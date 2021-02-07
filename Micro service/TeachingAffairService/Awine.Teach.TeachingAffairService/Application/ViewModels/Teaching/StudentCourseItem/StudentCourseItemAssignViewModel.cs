using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 报读课程分配班级 -> 视图模型
    /// </summary>
    public class StudentCourseItemAssignViewModel
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
        [Required(AllowEmptyStrings = false, ErrorMessage = "唯一标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "唯一标识不正确")]
        public string ClassesId { get; set; }
    }
}
