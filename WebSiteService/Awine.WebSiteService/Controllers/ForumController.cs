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
    /// 版块管理
    /// </summary>
    public class ForumController : ApiController
    {
        /// <summary>
        /// IForumService
        /// </summary>
        private readonly IForumService _formService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="formService"></param>
        public ForumController(IForumService formService)
        {
            _formService = formService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="displayAttribute"></param>
        /// <param name="contentAttribute"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, int displayAttribute = 0, int contentAttribute = 0)
        {
            return Response(success: true, data: await _formService.GetPageList(page, limit, displayAttribute, contentAttribute));
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(int displayAttribute = 0, int contentAttribute = 0)
        {
            return Response(success: true, data: await _formService.GetAll(displayAttribute, contentAttribute));
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("childs")]
        public async Task<IActionResult> GetAllChilds(string parentId = "")
        {
            return Response(success: true, data: await _formService.GetAllChilds(parentId));
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetModel(string id = "")
        {
            return Response(success: true, data: await _formService.GetModel(id));
        }

        /// <summary>
        /// 树型列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet("treelist")]
        public async Task<IActionResult> GetTreeList(string parentId = "")
        {
            return Response(success: true, data: await _formService.GetTreeList(parentId));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<IActionResult> Add([FromForm]ForumAddViewModel model)
        {
            var result = await _formService.Add(model);
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
        public async Task<IActionResult> Put([FromForm]ForumUpdateViewModel model)
        {
            var result = await _formService.Update(model);
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
        [Authorize]
        public async Task<IActionResult> Delete([FromForm]string id)
        {
            var result = await _formService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}