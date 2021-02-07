using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.OperationService.Application.ViewModels
{
    /// <summary>
    /// 反馈信息 -> 提交 ->视图模型
    /// </summary>
    public class FeedbackAddViewModel
    {
        /// <summary>
        /// 反馈信息
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "反馈信息必填")]
        [StringLength(200, ErrorMessage = "反馈信息长度为2-200个字符", MinimumLength = 2)]
        public string FeedbackMessage { get; set; }
    }
}
