using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Awine.Framework.Logger
{
    /// <summary>
    /// 异常信息实体
    /// </summary>
    public class KafkaLogException
    {
        /// <summary>
        /// 类名
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// 堆栈信息
        /// </summary>
        [JsonPropertyName("stack_trace")]
        public string StackTrace { get; set; }
    }
}
