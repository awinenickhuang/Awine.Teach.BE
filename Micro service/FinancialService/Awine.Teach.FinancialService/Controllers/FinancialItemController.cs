using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.FinancialService.Application.Interfaces;
using Awine.Teach.FinancialService.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.FinancialService.Controllers
{
    /// <summary>
    /// 财务收支项目
    /// </summary>
    public class FinancialItemController : ApiController
    {
        /// <summary>
        /// IFinancialItemService
        /// </summary>
        private readonly IFinancialItemService _financialItemService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="financialItemService"></param>
        public FinancialItemController(IFinancialItemService financialItemService)
        {
            _financialItemService = financialItemService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, int status = 0)
        {
            return Response(success: true, data: await _financialItemService.GetPageList(page, limit, status));
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(int status = 0)
        {
            return Response(success: true, data: await _financialItemService.GetAll(status));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]FinancialItemAddViewModel model)
        {
            var result = await _financialItemService.Add(model);
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
        public async Task<IActionResult> Update([FromForm]FinancialItemUpdateViewModel model)
        {
            var result = await _financialItemService.Update(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 更新 -> 启用状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatestatus")]
        public async Task<IActionResult> UpdateStatus([FromForm]FinancialItemUpdateStatusViewModel model)
        {
            var result = await _financialItemService.UpdateStatus(model);
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
            var result = await _financialItemService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}