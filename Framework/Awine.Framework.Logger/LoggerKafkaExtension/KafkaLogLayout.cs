﻿using log4net.Core;
using log4net.Layout;
using System;
using System.IO;
using System.Net;

namespace Awine.Framework.Logger
{
    /// <summary>
    /// Log4net Kafka日志模板
    /// </summary>
    public class KafkaLogLayout : LayoutSkeleton
    {
        /// <summary>
        /// AppId 区分日志来源
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 默认 IgnoresException 为 true，这里设置为 false
        /// </summary>
        public override void ActivateOptions()
        {
            IgnoresException = false;
        }

        /// <summary>
        /// 日志格式化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="loggingEvent"></param>
        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var kafkaLog = GetKafkaLog(loggingEvent);

            var message = System.Text.Json.JsonSerializer.Serialize(kafkaLog);

            writer.Write(message);
        }

        /// <summary>
        /// LoggingEvent to KafkaLog
        /// </summary>
        /// <param name="loggingEvent"></param>
        /// <returns></returns>
        private KafkaLog GetKafkaLog(LoggingEvent loggingEvent)
        {
            var obj = new KafkaLog
            {
                LogTimestamp = Convert.ToInt64((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds),
                AppId = AppId,
                HostName = Dns.GetHostName(),
                Level = loggingEvent.Level.ToString(),
                LoggerName = loggingEvent.LoggerName,
                Message = loggingEvent.RenderedMessage
            };

            if (loggingEvent.ExceptionObject != null)
            {
                obj.Exception = new KafkaLogException
                {
                    Name = loggingEvent.ExceptionObject.GetType().ToString(),
                    Message = loggingEvent.ExceptionObject.Message,
                    StackTrace = loggingEvent.ExceptionObject.StackTrace
                };
            }
            return obj;
        }
    }
}
