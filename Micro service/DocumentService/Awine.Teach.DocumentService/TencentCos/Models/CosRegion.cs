using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.DocumentService.TencentCos
{
    /// <summary>
    /// 
    /// </summary>
    public class CosRegion
    {
        /// <summary>
        /// 地域/显示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地域简称/代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        public CosRegion(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
