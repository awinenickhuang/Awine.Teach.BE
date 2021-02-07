using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 系统账号 -> 更新密码 -> 视图模型
    /// </summary>
    public class UsersUpdatePasswordViewModel
    {
        /// <summary>
        /// 原密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "原密码必填")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "原密码只能输入只能输入英文字母、数字")]
        [StringLength(18, ErrorMessage = "原密码长度为6-18个字符", MinimumLength = 6)]
        public string OriginalPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "新密码必填")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "新密码只能输入只能输入英文字母、数字")]
        [StringLength(18, ErrorMessage = "新密码长度为6-18个字符", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
