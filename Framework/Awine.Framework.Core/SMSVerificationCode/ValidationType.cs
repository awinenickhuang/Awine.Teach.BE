using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Framework.Core.SMSVerificationCode
{
    /// <summary>
    /// 短信验证类型
    /// </summary>
    public enum ValidationType
    {
        /// <summary>
        /// 类型未知
        /// </summary>
        UnKnow = 0,

        /// <summary>
        /// 注册
        /// </summary>
        Register = 1,

        /// <summary>
        /// 登录
        /// </summary>
        Login = 2,

        /// <summary>
        /// 通过手机号找回密码
        /// </summary>
        ResetPassword = 3,

        /// <summary>
        /// 修改手机号
        /// </summary>
        ChangePhoneNo = 4,

        /// <summary>
        /// 验证
        /// </summary>
        Validate = 5
    }
}
