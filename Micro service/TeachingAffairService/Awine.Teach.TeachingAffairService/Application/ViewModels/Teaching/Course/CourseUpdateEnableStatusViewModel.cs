using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 课程 -> 更新启用状态 ->视图模型
    /// </summary>
    public class CourseUpdateEnableStatusViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "唯一标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "唯一标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 课程状态 1-启用 2-停用
        /// </summary>
        [Required(ErrorMessage = "课程状态必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "课程状态只能输入正整数")]
        public int EnabledStatus { get; set; }
    }
}
