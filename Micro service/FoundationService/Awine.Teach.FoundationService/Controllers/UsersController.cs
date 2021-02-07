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
    /// 用户管理
    /// </summary>
    public class UsersController : ApiController
    {
        /// <summary>
        /// 用户
        /// </summary>
        private readonly IUsersService _usersService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUsersService userService)
        {
            _usersService = userService;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(bool isActive = true, int gender = 0)
        {
            var result = await _usersService.GetAll(isActive, gender);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="userName">姓名</param>
        /// <param name="phoneNumber">电话号码</param>
        /// <param name="tenantId">租户ID</param>
        /// <param name="departmentId">部门ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int pageIndex = 1, int pageSize = 15, string userName = "", string phoneNumber = "", string tenantId = "", string departmentId = "", string roleId = "")
        {
            var result = await _usersService.GetPageList(pageIndex, pageSize, userName, phoneNumber, tenantId, departmentId, roleId);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 查询某一部门的所有用户
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="isActive"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("allindepartment")]
        public async Task<IActionResult> GetAllInDepartment(string departmentId, bool isActive = true, int gender = 0)
        {
            var result = await _usersService.GetAllInDepartment(departmentId, isActive, gender);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">账号ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _usersService.GetModel(id));
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] UsersAddViewModel model)
        {
            var result = await _usersService.Add(model);
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
        public async Task<IActionResult> Update([FromForm] UsersUpdateViewModel model)
        {
            var result = await _usersService.Update(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 启用或禁用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("enableordisable")]
        public async Task<IActionResult> EnableOrDisable([FromForm] UsersUpdateStatusViewModel model)
        {
            var result = await _usersService.EnableOrDisable(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromForm] UsersResetPasswordViewModel model)
        {
            var result = await _usersService.ResetPassword(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetModel()
        {
            return Response(success: true, data: await _usersService.GetModel());
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("updatepassword")]
        public async Task<IActionResult> UpdatePassword([FromForm] UsersUpdatePasswordViewModel model)
        {
            var result = await _usersService.UpdatePassword(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}