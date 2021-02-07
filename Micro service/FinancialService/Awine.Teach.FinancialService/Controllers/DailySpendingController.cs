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
    /// 日常支出
    /// </summary>
    public class DailySpendingController : ApiController
    {
        /// <summary>
        /// IDailySpendingService
        /// </summary>
        private readonly IDailySpendingService _dailySpendingService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dailySpendingService"></param>
        public DailySpendingController(IDailySpendingService dailySpendingService)
        {
            _dailySpendingService = dailySpendingService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <param name="financialItemId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string beginDate = "", string finishDate = "", string financialItemId = "")
        {
            return Response(success: true, data: await _dailySpendingService.GetPageList(page, limit, beginDate, finishDate, financialItemId));
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="financialItemId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(string financialItemId = "")
        {
            return Response(success: true, data: await _dailySpendingService.GetAll(financialItemId));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]DailySpendingAddViewModel model)
        {
            var result = await _dailySpendingService.Add(model);
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
        public async Task<IActionResult> Update([FromForm]DailySpendingUpdateViewModel model)
        {
            var result = await _dailySpendingService.Update(model);
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
            var result = await _dailySpendingService.Delete(id);
            if (result > 0)
            {
                return Response(success: true);
            }
            return Response(success: false);
        }
    }
}