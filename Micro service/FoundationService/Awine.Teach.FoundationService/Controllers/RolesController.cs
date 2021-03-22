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
    /// 角色管理
    /// </summary>
    public class RolesController : ApiController
    {
        /// <summary>
        /// 角色
        /// </summary>
        private readonly IRolesService _rolesService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rolesService"></param>
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每页记录条数</param>
        /// <param name="tenantId">租户ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string tenantId = "")
        {
            var result = await _rolesService.GetPageList(page, limit, tenantId);

            return Response(success: true, data: result);
        }

        /// <summary>
        /// 所有角色
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(string tenantId = "")
        {
            return Response(success: true, data: await _rolesService.GetAll(tenantId));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]RolesAddViewModel model)
        {
            var result = await _rolesService.Add(model);

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
        public async Task<IActionResult> Put([FromForm]RolesUpdateViewModel model)
        {
            var result = await _rolesService.Update(model);
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
        public async Task<IActionResult> Delete([FromForm]string id)
        {
            var result = await _rolesService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 保存 -> 角色拥有的模块信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("saveroleownedmodules")]
        public async Task<IActionResult> SaveRoleOwnedModules([FromForm]RolesOwnedModulesAddViewModel model)
        {
            var result = await _rolesService.SaveRoleOwnedModules(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 查询 -> 角色拥有的模块信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getroleownedmodules")]
        public async Task<IActionResult> GetRoleOwnedModules(string roleId)
        {
            var result = await _rolesService.GetRoleOwnedModules(roleId);
            return Response(data: result);
        }
    }
}