using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.TeachingAffairService.Controllers
{
    /// <summary>
    /// 营销渠道
    /// </summary>
    public class MarketingChannelController : ApiController
    {
        /// <summary>
        /// IMarketingChannelService
        /// </summary>
        private readonly IMarketingChannelService _marketingChannelService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marketingChannelService"></param>
        public MarketingChannelController(IMarketingChannelService marketingChannelService)
        {
            _marketingChannelService = marketingChannelService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每页记录条数</param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15)
        {
            return Response(success: true, data: await _marketingChannelService.GetPageList(page, limit));
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            return Response(success: true, data: await _marketingChannelService.GetAll());
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]MarketingChannelAddViewModel model)
        {
            var result = await _marketingChannelService.Add(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Put([FromForm]MarketingChannelUpdateViewModel model)
        {
            var result = await _marketingChannelService.Update(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromForm]string id)
        {
            var result = await _marketingChannelService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}