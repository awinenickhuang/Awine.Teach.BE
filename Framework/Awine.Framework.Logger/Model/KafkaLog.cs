using System.Text.Json.Serialization;

namespace Awine.Framework.Logger
{
    /// <summary>
    /// 日志模板
    /// </summary>
    public class KafkaLog
    {
        /// <summary>
        /// 记录日志时的时间
        /// </summary>
        [JsonPropertyName("log_timestamp")]
        public long LogTimestamp { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        [JsonPropertyName("app_id")]
        public string AppId { get; set; }

        /// <summary>
        /// 主机名
        /// </summary>
        [JsonPropertyName("host_name")]
        public string HostName { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        [JsonPropertyName("level")]
        public string Level { get; set; }

        /// <summary>
        /// 日志名
        /// </summary>
        [JsonPropertyName("logger_name")]
        public string LoggerName { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Path
        /// </summary>
        [JsonPropertyName("request-path")]
        public string RequestPath { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        [JsonPropertyName("request-parameter")]
        public dynamic RequestParameter { get; set; }

        /// <summary>
        /// 请求响应时长 - Second
        /// </summary>
        [JsonPropertyName("request-response-time")]
        public long ResponseTime { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [JsonPropertyName("exception")]
        public KafkaLogException Exception { get; set; }
    }
}
