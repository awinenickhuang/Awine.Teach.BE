using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Awine.WebSite.Applicaton.Interface;
using Awine.WebSite.Applicaton.ViewModels;
using Awine.WebSite.Applicaton.ViewModels.Banner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.WebSiteService.Controllers
{
    /// <summary>
    /// 横幅图片
    /// </summary>
    public class BannerController : ApiController
    {
        /// <summary>
        /// IBannerService
        /// </summary>
        private readonly IBannerService _bannerService;

        /// <summary>
        /// IWebHostEnvironment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bannerService"></param>
        /// <param name="webHostEnvironment"></param>
        public BannerController(IBannerService bannerService, IWebHostEnvironment webHostEnvironment)
        {
            _bannerService = bannerService;
            _webHostEnvironment = webHostEnvironment;
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
            return Response(success: true, data: await _bannerService.GetPageList(page, limit));
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            return Response(success: true, data: await _bannerService.GetAll());
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<IActionResult> Add([FromForm]BannerAddViewModel model)
        {
            var result = await _bannerService.Add(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromForm]BannerDeleteViewModel model)
        {
            if (!string.IsNullOrEmpty(model.FilePath))
            {
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                string filePath = Path.Combine(contentRootPath + "/wwwroot" + model.FilePath);
                FileInfo file = new FileInfo(filePath);
                try
                {
                    var result = await _bannerService.Delete(model.Id);

                    if (result.Success)
                    {
                        if (file.Exists)
                        {
                            file.Delete();
                        }

                        return Response(success: true, message: result.Message);
                    }

                    return Response(success: false, message: result.Message);

                }
                catch (Exception ex)
                {
                    return Response(success: false, message: ex.Message);
                }
            }
            return Response(success: false, message: "操作失败");
        }
    }
}