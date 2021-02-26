using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Controllers.Teaching
{
    /// <summary>
    /// 学生成长记录评论
    /// </summary>
    public class StudentGrowthRecordCommentController : ApiController
    {
        /// <summary>
        /// IStudentGrowthRecordCommentService
        /// </summary>
        private readonly IStudentGrowthRecordCommentService _studentGrowthRecordCommentService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="studentGrowthRecordCommentService"></param>
        public StudentGrowthRecordCommentController(IStudentGrowthRecordCommentService studentGrowthRecordCommentService)
        {
            _studentGrowthRecordCommentService = studentGrowthRecordCommentService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="studentGrowthRecordId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string studentGrowthRecordId = "")
        {
            return Response(success: true, data: await _studentGrowthRecordCommentService.GetPageList(page, limit, studentGrowthRecordId));
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
            return Response(success: true, data: await _studentGrowthRecordCommentService.GetModel(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] StudentGrowthRecordCommentAddViewModel model)
        {
            var result = await _studentGrowthRecordCommentService.Add(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}
