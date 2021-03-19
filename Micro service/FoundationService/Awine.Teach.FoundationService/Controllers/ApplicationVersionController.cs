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
    /// 应用版本管理
    /// </summary>
    public class ApplicationVersionController : ApiController
    {
        /// <summary>
        /// IApplicationVersionService
        /// </summary>
        private readonly IApplicationVersionService _applicationVersionService;

        /// <summary>
        /// ApplicationVersionController
        /// </summary>
        /// <param name="applicationVersionService"></param>
        public ApplicationVersionController(IApplicationVersionService applicationVersionService)
        {
            _applicationVersionService = applicationVersionService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        [HttpGet("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string name = "", string identifying = "")
        {
            var result = await _applicationVersionService.GetPageList(page, limit, name, identifying);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetAll(string name = "", string identifying = "")
        {
            var result = await _applicationVersionService.GetAll(name, identifying);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] ApplicationVersionAddViewModel model)
        {
            var result = await _applicationVersionService.Add(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">按钮ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _applicationVersionService.GetModel(id));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">按钮ID</param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromForm] string id)
        {
            var result = await _applicationVersionService.Delete(id);
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
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] ApplicationVersionUpdateViewModel model)
        {
            var result = await _applicationVersionService.Update(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}
