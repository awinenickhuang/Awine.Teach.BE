using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.WebSite.Applicaton.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ManagerLoginViewModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号必填")]
        [StringLength(16, ErrorMessage = "账号长度为1-16个字符", MinimumLength = 1)]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码必填")]
        [StringLength(18, ErrorMessage = "密码长度为6-18个字符", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
