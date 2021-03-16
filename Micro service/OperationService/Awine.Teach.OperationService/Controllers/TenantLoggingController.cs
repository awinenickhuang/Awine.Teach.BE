using Awine.Teach.OperationService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.OperationService.Controllers
{
    /// <summary>
    /// 租户登录日志
    /// </summary>
    public class TenantLoggingController : ApiController
    {
        /// <summary>
        /// ITenantLoggingService
        /// </summary>
        private readonly ITenantLoggingService _tenantLoggingService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tenantLoggingService"></param>
        public TenantLoggingController(ITenantLoggingService tenantLoggingService)
        {
            _tenantLoggingService = tenantLoggingService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="account"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string account = "", string tenantId = "")
        {
            return Response(success: true, data: await _tenantLoggingService.GetPageList(page, limit, account, tenantId));
        }
    }
}
