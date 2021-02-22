using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Controllers.Teaching
{
    /// <summary>
    /// 相册管理-> 相片管理
    /// </summary>
    public class ClassPhotoalbumAttachmentController : ApiController
    {
        /// <summary>
        /// 相册管理-> 相片管理
        /// </summary>
        private readonly IClassPhotoalbumAttachmentService _classPhotoalbumAttachmentService;

        /// <summary>
        /// 构造 - 注入需要的资源
        /// </summary>
        /// <param name="classPhotoalbumAttachmentService"></param>
        public ClassPhotoalbumAttachmentController(IClassPhotoalbumAttachmentService classPhotoalbumAttachmentService)
        {
            _classPhotoalbumAttachmentService = classPhotoalbumAttachmentService;
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="photoalbumId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string photoalbumId = "")
        {
            return Response(success: true, data: await _classPhotoalbumAttachmentService.GetPageList(page, limit, photoalbumId));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] ClassPhotoalbumAttachmentAddViewModel model)
        {
            var result = await _classPhotoalbumAttachmentService.Add(model);
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
            var result = await _classPhotoalbumAttachmentService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}
