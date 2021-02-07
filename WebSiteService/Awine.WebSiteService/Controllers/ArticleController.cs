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
    /// 文章管理
    /// </summary>
    public class ArticleController : ApiController
    {
        /// <summary>
        /// IArticleService
        /// </summary>
        private readonly IArticleService _articleService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="articleService"></param>
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="forumId"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="contentSource"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string forumId = "", string title = "", string author = "", string contentSource = "")
        {
            return Response(success: true, data: await _articleService.GetPageList(page, limit, forumId, title, author, contentSource));
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="forumId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(string forumId, int number = 0)
        {
            return Response(success: true, data: await _articleService.GetAll(forumId, number));
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _articleService.GetModel(id));
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("single")]
        public async Task<IActionResult> GetModelForumAccording(string forumId)
        {
            return Response(success: true, data: await _articleService.GetModelForumAccording(forumId));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<IActionResult> Add([FromForm]ArticleAddViewModel model)
        {
            var result = await _articleService.Add(model);
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
        public async Task<IActionResult> Put([FromForm]ArticleUpdateViewModel model)
        {
            var result = await _articleService.Update(model);
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
            var result = await _articleService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}