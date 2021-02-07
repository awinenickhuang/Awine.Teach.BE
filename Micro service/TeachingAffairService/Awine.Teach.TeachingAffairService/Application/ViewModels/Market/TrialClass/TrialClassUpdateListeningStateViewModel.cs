using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 试听管理 -> 更新到课状态 -> 视图模型
    /// </summary>
    public class TrialClassUpdateListeningStateViewModel
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
}
