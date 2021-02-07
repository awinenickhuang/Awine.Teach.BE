using Awine.Framework.Logger.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Awine.Framework.Logger
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Automatically record log the run of each Action
        /// </summary>
        /// <param name="applicationBuilder"></param>
        public static void UseHttpRequestGlobalMiddleWare(this IApplicationBuilder applicationBuilder, Action<LoggerOptions> loggerOptions = null)
        {
            //var options = new LoggerOptions();
            //loggerOptions?.Invoke(options);
            //applicationBuilder.Services.AddSingleton(options);

            applicationBuilder.UseMiddleware(typeof(HttpRequestGlobalMiddleWare));
        }

        /// <summary>
        /// Use logging system
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder AddCustomLogger(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureLogging((context, loggingBuilder) =>
            {
                loggingBuilder.AddFilter("System", LogLevel.Warning);//过滤掉系统默认的一些日志
                loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
                loggingBuilder.SetMinimumLevel(LogLevel.Information);
                loggingBuilder.AddLog4Net("log4net.config"); //不带参数：表示log4net.config的配置文件就在应用程序根目录下
            });
        }
    }
}
