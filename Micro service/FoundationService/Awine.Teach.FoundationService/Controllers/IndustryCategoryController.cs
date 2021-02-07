using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.FoundationService.Controllers
{
    /// <summary>
    /// 行业管理
    /// </summary>
    public class IndustryCategoryController : ApiController
    {
        /// <summary>
        /// 行业
        /// </summary>
        private readonly IIndustryCategoryService _industryCategoryService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="industryCategoryService"></param>
        public IndustryCategoryController(IIndustryCategoryService industryCategoryService)
        {
            _industryCategoryService = industryCategoryService;
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
            return Response(success: true, data: await _industryCategoryService.GetPageList(page, limit));
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            return Response(success: true, data: await _industryCategoryService.GetAll());
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] IndustryCategoryAddViewModel model)
        {
            var result = await _industryCategoryService.Add(model);
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
        public async Task<IActionResult> Put([FromForm] IndustryCategoryViewModel model)
        {
            var result = await _industryCategoryService.Update(model);
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
        public async Task<IActionResult> Delete([FromForm] string id)
        {
            var result = await _industryCategoryService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}