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
    /// 班级相册
    /// </summary>
    public class ClassPhotoalbumController : ApiController
    {
        /// <summary>
        /// IClassPhotoalbumService
        /// </summary>
        private readonly IClassPhotoalbumService _classPhotoalbumService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="classPhotoalbumService"></param>
        public ClassPhotoalbumController(IClassPhotoalbumService classPhotoalbumService)
        {
            _classPhotoalbumService = classPhotoalbumService;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="visibleRange"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(string classId = "", int visibleRange = 0, string startDate = "", string endDate = "")
        {
            return Response(success: true, data: await _classPhotoalbumService.GetAll(classId, visibleRange, startDate, endDate));
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="classId"></param>
        /// <param name="visibleRange"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string classId = "", int visibleRange = 0, string startDate = "", string endDate = "")
        {
            return Response(success: true, data: await _classPhotoalbumService.GetPageList(page, limit, classId, visibleRange, startDate, endDate));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] ClassPhotoalbumAddViewModel model)
        {
            var result = await _classPhotoalbumService.Add(model);
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
        public async Task<IActionResult> Put([FromForm] ClassPhotoalbumUpdateViewModel model)
        {
            var result = await _classPhotoalbumService.Update(model);
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
            var result = await _classPhotoalbumService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}
