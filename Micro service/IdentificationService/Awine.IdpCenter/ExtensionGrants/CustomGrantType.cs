using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.ExtensionGrants
{
    /// <summary>
    /// 扩展登录方式
    /// </summary>
    public class CustomGrantType
    {
        /// <summary>
        /// 手机验证码登录
        /// </summary>
        public const string Sms = "sms";

        /// <summary>
        /// 外部扩展登录
        /// </summary>
        public const string External = "external";
    }
}
