using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.OperationService.Application.Interfaces;
using Awine.Teach.OperationService.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.OperationService.Controllers
{
    /// <summary>
    /// 行业资讯
    /// </summary>
    public class NewsController : ApiController
    {
        /// <summary>
        /// INewsService
        /// </summary>
        private readonly INewsService _newsService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newsService"></param>
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15)
        {
            return Response(success: true, data: await _newsService.GetPageList(page, limit));
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("single")]
        [AllowAnonymous]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _newsService.GetModel(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]NewsAddViewModel model)
        {
            var result = await _newsService.Add(model);
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
            var result = await _newsService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}