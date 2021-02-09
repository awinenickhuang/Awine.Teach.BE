using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Awine.Teach.DocumentService.Controllers
{
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// 统一返回格式
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="success"></param>
        /// <returns></returns>
        protected new IActionResult Response(object data = null, string message = "请求成功", bool success = true)
        {
            if (success)
            {
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    success = true,
                    data = data,
                    message = message
                });
            }

            return BadRequest(new
            {
                statusCode = HttpStatusCode.BadRequest,
                success = false,
                message = message
            });
        }
    }
}
