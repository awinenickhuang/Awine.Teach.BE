using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Framework.AspNetCore.Authorize;
using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.FoundationService.Controllers
{
    /// <summary>
    /// 平台租户
    /// </summary>
    public class TenantsController : ApiController
    {
        /// <summary>
        /// 租户信息
        /// </summary>
        private readonly ITenantsService _tenantsService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tenantsService"></param>
        public TenantsController(ITenantsService tenantsService)
        {
            _tenantsService = tenantsService;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="classiFication"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="status"></param>
        /// <param name="industryId"></param>
        /// <param name="creatorId"></param>
        /// <param name="creatorTenantId"></param>
        /// <returns></returns>
        [HttpGet("list")]
        //[AuthorizeCode("TenantList")]
        public async Task<IActionResult> GetTreeList(int classiFication = 0, string saaSVersionId = "", int status = 0, string industryId = "", string creatorId = "", string creatorTenantId = "")
        {
            return Response(success: true, data: await _tenantsService.GetAll(classiFication, saaSVersionId, status, industryId, creatorId, creatorTenantId));
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="classiFication"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="status"></param>
        /// <param name="industryId"></param>
        /// <param name="creatorId"></param>
        /// <param name="creatorTenantId"></param>
        /// <returns></returns>
        [HttpGet("pagelist")]
        //[AuthorizeCode("TenantPageList")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, int classiFication = 0, string saaSVersionId = "", int status = 0, string industryId = "", string creatorId = "", string creatorTenantId = "")
        {
            return Response(success: true, data: await _tenantsService.GetPageList(page, limit, classiFication, saaSVersionId, status, industryId, creatorId, creatorTenantId));
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("single")]
        //[AuthorizeCode("TenantGetModel")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _tenantsService.GetModel(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        //[AuthorizeCode("TenantAdd")]
        public async Task<IActionResult> Add([FromForm] TenantsAddViewModel model)
        {
            var result = await _tenantsService.Add(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 更新 -> 基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("update")]
        //[AuthorizeCode("TenantUpdate")]
        public async Task<IActionResult> Put([FromForm] TenantsUpdateViewModel model)
        {
            var result = await _tenantsService.Update(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 更新 -> 租户状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("updatestatus")]
        //[AuthorizeCode("TenantUpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromForm] TenantsUpdateStatusViewModel model)
        {
            var result = await _tenantsService.UpdateStatus(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        #region 开放接口

        /// <summary>
        /// 机构入驻 -> 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("enter")]
        [AllowAnonymous]
        public async Task<IActionResult> Enter([FromForm] TenantsAddViewModel model)
        {
            var result = await _tenantsService.Add(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        #endregion
    }
}