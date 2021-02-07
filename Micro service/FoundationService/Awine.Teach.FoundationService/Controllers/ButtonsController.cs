using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.FoundationService.Controllers
{
    /// <summary>
    /// 按钮管理
    /// </summary>
    public class ButtonsController : ApiController
    {
        /// <summary>
        /// IButtonsService
        /// </summary>
        private readonly IButtonsService _buttonsService;

        /// <summary>
        /// SysButtonsController
        /// </summary>
        /// <param name="buttonsService"></param>
        public ButtonsController(IButtonsService buttonsService)
        {
            _buttonsService = buttonsService;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("btnlist")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _buttonsService.GetAll();
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 某一模块拥有的按钮
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetModuleButtons(string moduleId)
        {
            var result = await _buttonsService.GetModuleButtons(moduleId);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 某一模块拥有的按钮 -> 分页列表
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("pagelist")]
        public async Task<IActionResult> GetPageList(string moduleId, int page = 1, int limit = 15)
        {
            var result = await _buttonsService.GetPageList(page, limit, moduleId);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] ButtonsAddViewModel model)
        {
            var result = await _buttonsService.Add(model);
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
            return Response(success: true, data: await _buttonsService.GetModel(id));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">按钮ID</param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromForm]string id)
        {
            var result = await _buttonsService.Delete(id);
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
        public async Task<IActionResult> Update([FromForm]ButtonsViewModel model)
        {
            var result = await _buttonsService.Update(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}