using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.DataSecurity
{
    /// <summary>
    /// RSA算法类型枚举
    /// </summary>
    public enum EnumRSAType
    {
        /// <summary>
        /// SHA1
        /// </summary>
        RSA = 0,

        /// <summary>
        /// RSA2 密钥长度至少为2048
        /// SHA256
        /// </summary>
        RSA2
    }
}
