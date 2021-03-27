using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.DocumentService.TencentCos
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteError : CosEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
    }
}
