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
    /// 部门管理
    /// </summary>
    public class DepartmentsController : ApiController
    {
        /// <summary>
        /// IDepartmentService
        /// </summary>
        private readonly IDepartmentsService _departmentsService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="departmentsService"></param>
        public DepartmentsController(IDepartmentsService departmentsService)
        {
            _departmentsService = departmentsService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="tenantId">每页记录条数</param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int pageIndex = 1, int pageSize = 15, string tenantId = "")
        {
            var result = await _departmentsService.GetPageList(pageIndex, pageSize, tenantId);
            return Response(success: true, data: result);
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(string tenantId = "")
        {
            return Response(success: true, data: await _departmentsService.GetAll(tenantId));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm]DepartmentsAddViewModel model)
        {
            var result = await _departmentsService.Add(model);
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
        public async Task<IActionResult> Put([FromForm]DepartmentsUpdateViewModel model)
        {
            var result = await _departmentsService.Update(model);
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
        public async Task<IActionResult> Delete([FromForm]string id)
        {
            var result = await _departmentsService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}