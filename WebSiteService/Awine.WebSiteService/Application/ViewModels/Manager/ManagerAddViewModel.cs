using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.WebSite.Applicaton.ViewModels
{
    /// <summary>
    /// 管理员 -> 添加 -> 视图模型
    /// </summary>
    public class ManagerAddViewModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名必填"), MaxLength(6, ErrorMessage = "姓名太长啦"), MinLength(2, ErrorMessage = "姓名太短啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "姓名必须是中文")]
        public string Name { get; set; }

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
