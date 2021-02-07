using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.OperationService.Controllers
{
    /// <summary>
    /// 服务健康检查
    /// </summary>
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 默认健康检查实现
        /// </summary>
        /// <returns></returns>
        [Route("/health")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string HealthCheck()
        {
            return "OK";
        }
    }
}