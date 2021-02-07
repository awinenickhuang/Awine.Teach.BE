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
    /// 模块管理
    /// </summary>
    public class ModulesController : ApiController
    {
        /// <summary>
        /// IModulesService
        /// </summary>
        private readonly IModulesService _modulesService;

        /// <summary>
        /// ClientModulesController
        /// </summary>
        /// <param name="modulesService"></param>
        public ModulesController(IModulesService modulesService)
        {
            _modulesService = modulesService;
        }

        /// <summary>
        /// 普通列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            return Response(success: true, data: await _modulesService.GetAll());
        }

        /// <summary>
        /// 带选中状态的列表 -> 设置角色权限
        /// </summary>
        /// <param name="roleId">待设置角色</param>
        /// <returns></returns>
        [HttpGet("listwithchedkedstatus")]
        public async Task<IActionResult> GetAllWithChedkedStatus(string roleId)
        {
            return Response(success: true, data: await _modulesService.GetAllWithChedkedStatus(roleId));
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15)
        {
            var result = await _modulesService.GetPageList(page, limit);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 树型列表
        /// </summary>
        /// <param name="moduleParentId"></param>
        /// <returns></returns>
        [HttpGet("treelist")]
        public async Task<IActionResult> GetTreeList(string moduleParentId = "")
        {
            return Response(success: true, data: await _modulesService.GetTreeList(moduleParentId));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] ModulesAddViewModel model)
        {
            var result = await _modulesService.Add(model);
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
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromForm] string id)
        {
            var result = await _modulesService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _modulesService.GetModel(id));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] ModulesUpdateViewModel model)
        {
            var result = await _modulesService.Update(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 当前登录用户拥有的模块列表 -> 生成系统菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getroleownedmodules")]
        public async Task<IActionResult> GetRoleOwnedModules()
        {
            var result = await _modulesService.GetRoleOwnedModules();
            if (result.Success)
            {
                return Response(success: true, message: result.Message, data: result.Data);
            }
            return Response(success: false, message: result.Message);
        }
    }
}