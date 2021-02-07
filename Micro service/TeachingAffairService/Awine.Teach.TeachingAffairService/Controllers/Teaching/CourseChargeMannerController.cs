using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.TeachingAffairService.Controllers
{
    /// <summary>
    /// 课程收费方式
    /// </summary>
    public class CourseChargeMannerController : ApiController
    {
        /// <summary>
        /// ICourseChargeMannerService
        /// </summary>
        private readonly ICourseChargeMannerService _courseChargeMannerService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="courseChargeMannerService"></param>
        public CourseChargeMannerController(ICourseChargeMannerService courseChargeMannerService)
        {
            _courseChargeMannerService = courseChargeMannerService;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(string courseId)
        {
            return Response(success: true, data: await _courseChargeMannerService.GetAll(courseId));
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string courseId = "")
        {
            return Response(success: true, data: await _courseChargeMannerService.GetPageList(page, limit, courseId));
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _courseChargeMannerService.GetModel(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]CourseChargeMannerAddViewModel model)
        {
            var result = await _courseChargeMannerService.Add(model);

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
        public async Task<IActionResult> Put([FromForm]CourseChargeMannerUpdateViewModel model)
        {
            var result = await _courseChargeMannerService.Update(model);

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
        public async Task<IActionResult> Delete([FromForm]string id)
        {
            var result = await _courseChargeMannerService.Delete(id);

            if (result > 0)
            {
                return Response(success: true, message: "操作成功");
            }

            return Response(success: false, message: "操作失败");
        }
    }
}