using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FinancialService.Application.ServiceResult
{
    /// <summary>
    /// 业务层返回结果 -> without result data
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 业务层返回结果 -> with result data
    /// </summary>
    public class Result<T> where T : class
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 结果数据
        /// </summary>
        public T Data { get; set; }
    }
}
