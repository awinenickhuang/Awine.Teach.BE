using Awine.Framework.Core.DomainInterface;
using Awine.Framework.Core.Exceptions;
using Awine.Framework.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Awine.Framework.AspNetCore.CustomException
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        /// <param name="logger"></param>
        public HttpGlobalExceptionFilter(IWebHostEnvironment webHostEnvironment, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._logger = logger;
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception.GetType() == typeof(AwineDomainException))
            {
                var problemDetails = new ValidationProblemDetails()
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "系统出现了一个错误！"
                };

                problemDetails.Errors.Add("DomainValidations", new string[] { context.Exception.Message.ToString() });

                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var response = new ExceptionResponse
                {
                    Success = false,
                    Message = "系统出现了一个错误！"
                };

                if (_webHostEnvironment.IsDevelopment())
                {
                    response.DeveloperMessage = context.Exception;
                }

                //context.Result = new InternalServerErrorObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }
    }
}
