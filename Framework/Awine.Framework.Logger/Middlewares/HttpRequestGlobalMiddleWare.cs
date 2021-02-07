using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Awine.Framework.Logger.Middlewares
{
    /// <summary>
    /// 全局异常处理
    /// 1-自动记录异常信息
    /// 2-全局异常返回统一格式
    /// </summary>
    public class HttpRequestGlobalMiddleWare
    {
        /// <summary>
        /// 处理HTTP请求的函数 RequestDelegate 是一种委托类型，其全貌为public delegate Task RequestDelegate(HttpContext context)
        /// </summary>
        private readonly RequestDelegate _nextDelegate;

        /// <summary>
        /// Provides information about the web hosting environment an application is running in.
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<HttpRequestGlobalMiddleWare> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nextDelegate"></param>
        /// <param name="webHostEnvironment"></param>
        /// <param name="logger"></param>
        public HttpRequestGlobalMiddleWare(RequestDelegate nextDelegate,
            IWebHostEnvironment webHostEnvironment,
            ILogger<HttpRequestGlobalMiddleWare> logger)
        {
            _nextDelegate = nextDelegate;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            KafkaLog log = new KafkaLog();
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();

                log.Message = "请求成功";
                log.RequestPath = context.Request.Path;

                //获取请求参数
                HttpRequest request = context.Request;
                var _data = new SortedDictionary<string, object>();

                if (request.Method.ToLower().Equals("post"))
                {
                    request.EnableBuffering();
                    Stream stream = request.Body;
                    byte[] buffer = new byte[request.ContentLength.Value];
                    await stream.ReadAsync(buffer, 0, buffer.Length);
                    _data.Add("request.body", Encoding.UTF8.GetString(buffer));
                    request.Body.Position = 0;
                }
                else if (request.Method.ToLower().Equals("get"))
                {
                    _data.Add("request.body", request.QueryString.Value);
                }
                log.RequestParameter = _data;

                await _nextDelegate(context);

                stopwatch.Stop();

                log.ResponseTime = stopwatch.ElapsedMilliseconds;

                //记录运行日志
                _logger.LogInformation(JsonSerializer.Serialize(log));
            }
            catch (Exception ex)
            {
                log.Exception = new KafkaLogException
                {
                    Name = ex.Source,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };

                //记录异常信息
                _logger.LogError(System.Text.Json.JsonSerializer.Serialize(log));

                //发生错误时，格式化返回信息
                var response = new ExceptionResponse
                {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
                if (_webHostEnvironment.IsDevelopment())
                {
                    response.Message = ex.Message;
                    response.DeveloperMessage = ex.StackTrace;
                }
                context.Response.ContentType = "text/json;charset=utf-8;";

                //序列化时，JSON 属性名称使用 camel 大小写
                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response, serializeOptions));
            }
        }
    }
}