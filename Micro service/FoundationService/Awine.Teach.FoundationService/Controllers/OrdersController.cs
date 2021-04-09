using Awine.Teach.FoundationService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Controllers
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class OrdersController : ApiController
    {
        /// <summary>
        /// IOrdersService
        /// </summary>
        private readonly IOrdersService _ordersService;

        /// <summary>
        /// OrdersController
        /// </summary>
        /// <param name="ordersService"></param>
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="tradeCategories"></param>
        /// <param name="performanceOwnerId"></param>
        /// <param name="performanceTenantId"></param>
        /// <returns></returns>
        [HttpGet("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string tenantId = "", string saaSVersionId = "", int tradeCategories = 0, string performanceOwnerId = "", string performanceTenantId = "")
        {
            var result = await _ordersService.GetPageList(page, limit, tenantId, saaSVersionId, tradeCategories, performanceOwnerId, performanceTenantId);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="tradeCategories"></param>
        /// <param name="performanceOwnerId"></param>
        /// <param name="performanceTenantId"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetAll(string tenantId = "", string saaSVersionId = "", int tradeCategories = 0, string performanceOwnerId = "", string performanceTenantId = "")
        {
            var result = await _ordersService.GetAll(tenantId, saaSVersionId, tradeCategories, performanceOwnerId, performanceTenantId);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">按钮ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _ordersService.GetModel(id));
        }
    }
}
