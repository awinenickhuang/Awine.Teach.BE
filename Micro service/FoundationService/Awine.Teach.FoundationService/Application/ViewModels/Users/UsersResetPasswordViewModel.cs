using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 系统账号 -> 更新密码（管理员） -> 视图模型
    /// </summary>
    public class UsersResetPasswordViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "主键标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "主键标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码必填")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "密码只能输入只能输入英文字母、数字")]
        [StringLength(32, ErrorMessage = "原密码长度为6-32个字符", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
