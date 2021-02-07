namespace Awine.Framework.Logger
{
    /// <summary>
    /// Kafka 参数配置
    /// </summary>
    public class KafkaSettings
    {
        /// <summary>
        /// Broker 多个使用 "," 分隔
        /// </summary>
        public string Broker { get; set; }

        /// <summary>
        /// Topic 主要用于区分日志来源于哪个不同的服务
        /// </summary>
        public string Topic { get; set; } = "log4net-test-topic";
    }
}
