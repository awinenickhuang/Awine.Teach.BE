using Confluent.Kafka;
using log4net.Appender;
using log4net.Core;
using System;
using System.IO;

namespace Awine.Framework.Logger
{
    /// <summary>
    /// log4net 提供了多种内置的 appender，配置使用哪种 appender，就会以对应 appender 的实现方式来记录日志
    /// appender 支持自定义，只要符合 appender 定义规则即可，在这里我们针对Kafka自定义一个appender
    /// </summary>
    public class KafkaAppender : AppenderSkeleton
    {
        /// <summary>
        /// Producer
        /// </summary>
        private IProducer<Null, string> _producer;

        /// <summary>
        /// Kafka 配置信息
        /// </summary>
        public KafkaSettings KafkaSettings { get; set; }

        /// <summary>
        /// 重写 ActivateOptions
        /// </summary>
        public override void ActivateOptions()
        {
            base.ActivateOptions();
            Init();
        }

        /// <summary>
        /// 注销
        /// </summary>
        protected override void OnClose()
        {
            base.OnClose();
            Dispose();
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            var message = GetMessage(loggingEvent);
            var topic = KafkaSettings.Topic;
            _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }

        /// <summary>
        ///  根据 KafkaLogLayout 模板获取日志信息
        /// </summary>
        /// <param name="loggingEvent"></param>
        /// <returns></returns>
        private string GetMessage(LoggingEvent loggingEvent)
        {
            using (var sr = new StringWriter())
            {
                Layout.Format(sr, loggingEvent);

                if (Layout.IgnoresException && loggingEvent.ExceptionObject != null)
                    sr.Write(loggingEvent.GetExceptionString());

                return sr.ToString();
            }
        }

        /// <summary>
        /// Kafka producer 初始化 
        /// </summary>
        private void Init()
        {
            try
            {
                if (KafkaSettings == null)
                    throw new LogException("KafkaSettings is missing");

                if (string.IsNullOrEmpty(KafkaSettings.Broker))
                    throw new Exception("Broker is missing");

                if (string.IsNullOrEmpty(KafkaSettings.Topic))
                    throw new Exception("Topic is missing");

                if (null == _producer)
                {
                    var config = new ProducerConfig
                    {
                        BootstrapServers = KafkaSettings.Broker
                    };
                    _producer = new ProducerBuilder<Null, string>(config).Build();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Error("could not init producer", ex);
            }
        }

        /// <summary>
        /// 释放 producer
        /// </summary>
        private void Dispose()
        {
            try
            {
                _producer?.Dispose();
            }
            catch (Exception ex)
            {
                ErrorHandler.Error("could not dispose producer", ex);
            }
        }
    }
}
