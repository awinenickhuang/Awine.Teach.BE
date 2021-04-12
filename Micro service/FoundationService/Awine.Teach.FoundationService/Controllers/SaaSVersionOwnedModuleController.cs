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
    /// SaaS版本包括的系统模块
    /// </summary>
    public class SaaSVersionOwnedModuleController : ApiController
    {
        /// <summary>
        /// SaaS版本包括的系统模块
        /// </summary>
        private ISaaSVersionOwnedModuleService _saaSVersionOwnedModuleService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="saaSVersionOwnedModuleService"></param>
        public SaaSVersionOwnedModuleController(ISaaSVersionOwnedModuleService saaSVersionOwnedModuleService)
        {
            _saaSVersionOwnedModuleService = saaSVersionOwnedModuleService;
        }

        /// <summary>
        /// 保存SaaS版本包括的模块信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("savesaasversionownedmodules")]
        public async Task<IActionResult> SaveSaaSVersionOwnedModules([FromForm] SaaSVersionOwnedModuleAddViewModel model)
        {
            var result = await _saaSVersionOwnedModuleService.SaveSaaSVersionOwnedModules(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 查询SaaS版本包括的模块信息
        /// </summary>
        /// <param name="saaSVersionId">SaaS版本ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("loadsaasversionownedmodules")]
        public async Task<IActionResult> GetSaaSVersionOwnedModules(string saaSVersionId)
        {
            var result = await _saaSVersionOwnedModuleService.GetSaaSVersionOwnedModules(saaSVersionId);
            return Response(data: result);
        }
    }
}
