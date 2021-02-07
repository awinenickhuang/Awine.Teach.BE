using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 更新报读课程学习进度 -> 视图模型
    /// </summary>
    public class UpdateLearningProcessViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "唯一标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "唯一标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 学习进度 1-已报名（未分班） 2-已报名（已分班）3-停课 4-退费 5-毕业
        /// </summary>
        [Required(ErrorMessage = "学习进度必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "学习进度只能输入正整数")]
        public int LearningProcess { get; set; }
    }
}
