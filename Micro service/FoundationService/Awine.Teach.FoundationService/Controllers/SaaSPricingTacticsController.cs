using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Controllers
{
    /// <summary>
    /// SaaS版本定价策略
    /// </summary>
    public class SaaSPricingTacticsController : ApiController
    {
        /// <summary>
        /// SaaS版本定价策略
        /// </summary>
        private readonly ISaaSPricingTacticsService _saaSPricingTacticsService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="saaSPricingTacticsService"></param>
        public SaaSPricingTacticsController(ISaaSPricingTacticsService saaSPricingTacticsService)
        {
            _saaSPricingTacticsService = saaSPricingTacticsService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        [HttpGet("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string saaSVersionId = "")
        {
            var result = await _saaSPricingTacticsService.GetPageList(page, limit, saaSVersionId);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetAll(string saaSVersionId = "")
        {
            var result = await _saaSPricingTacticsService.GetAll(saaSVersionId);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] SaaSPricingTacticsAddViewModel model)
        {
            var result = await _saaSPricingTacticsService.Add(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromForm] string id)
        {
            var result = await _saaSPricingTacticsService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}
