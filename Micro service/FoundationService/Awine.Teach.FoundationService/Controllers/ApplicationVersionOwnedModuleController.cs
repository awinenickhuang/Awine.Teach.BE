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
    /// 应用版本对应的系统模块
    /// </summary>
    public class ApplicationVersionOwnedModuleController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        private IApplicationVersionOwnedModuleService _applicationVersionOwnedModuleService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationVersionOwnedModuleService"></param>
        public ApplicationVersionOwnedModuleController(IApplicationVersionOwnedModuleService applicationVersionOwnedModuleService)
        {
            _applicationVersionOwnedModuleService = applicationVersionOwnedModuleService;
        }

        /// <summary>
        /// 保存应用版本包括的模块信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("saverappversionownedmodules")]
        public async Task<IActionResult> SaveAppVersionOwnedModules([FromForm] ApplicationVersionOwnedModuleAddViewModel model)
        {
            var result = await _applicationVersionOwnedModuleService.SaveAppVersionOwnedModules(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 查询应用版本包括的模块信息
        /// </summary>
        /// <param name="appVersionId">应用版本ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getappversionownedmodules")]
        public async Task<IActionResult> GetRoleOwnedModules(string appVersionId)
        {
            var result = await _applicationVersionOwnedModuleService.GetAppVersionOwnedModules(appVersionId);
            return Response(data: result);
        }
    }
}
