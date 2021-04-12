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
    /// 租户设置 - 默认配置
    /// </summary>
    public class TenantDefaultSettingsController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITenantDefaultSettingsService _tenantDefaultSettingsService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantDefaultSettingsService"></param>
        public TenantDefaultSettingsController(ITenantDefaultSettingsService tenantDefaultSettingsService)
        {
            _tenantDefaultSettingsService = tenantDefaultSettingsService;
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
            return Response(success: true, data: await _tenantDefaultSettingsService.GetPageList(page, limit));
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            return Response(success: true, data: await _tenantDefaultSettingsService.GetAll());
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("single")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _tenantDefaultSettingsService.GetModel(id));
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("singlesaasversion")]
        public async Task<IActionResult> GetModelForSaaSVersion(string saaSVersionId)
        {
            return Response(success: true, data: await _tenantDefaultSettingsService.GetModelForSaaSVersion(saaSVersionId));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] TenantDefaultSettingsAddViewModel model)
        {
            var result = await _tenantDefaultSettingsService.Add(model);
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
            var result = await _tenantDefaultSettingsService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromForm] TenantDefaultSettingsUpdateViewModel model)
        {
            var result = await _tenantDefaultSettingsService.Update(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}
