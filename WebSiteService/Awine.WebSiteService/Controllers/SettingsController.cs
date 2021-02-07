using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.WebSite.Applicaton.Interface;
using Awine.WebSite.Applicaton.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.WebSiteService.Controllers
{
    /// <summary>
    /// 网站设置
    /// </summary>
    public class SettingsController : ApiController
    {
        /// <summary>
        /// ISettingsService
        /// </summary>
        private readonly ISettingsService _settingsService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settingsService"></param>
        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
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
            return Response(success: true, data: await _settingsService.GetPageList(page, limit));
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            return Response(success: true, data: await _settingsService.GetAll());
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _settingsService.GetModel(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<IActionResult> Add([FromForm]SettingsAddViewModel model)
        {
            var result = await _settingsService.Add(model);

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
        [Authorize]
        public async Task<IActionResult> Update([FromForm]SettingsUpdateViewModel model)
        {
            var result = await _settingsService.Update(model);

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
        [Authorize]
        public async Task<IActionResult> Delete([FromForm]string id)
        {
            var result = await _settingsService.Delete(id);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }
    }
}