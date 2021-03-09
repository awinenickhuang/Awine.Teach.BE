using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.TeachingAffairService.Controllers.Teaching
{
    /// <summary>
    /// 学生成长记录
    /// </summary>
    public class StudentGrowthRecordController : ApiController
    {
        /// <summary>
        /// 学生成长记录
        /// </summary>
        private readonly IStudentGrowthRecordService _studentGrowthRecordService;

        /// <summary>
        /// 构造函数 -> 注入需要的资源
        /// </summary>
        /// <param name="studentGrowthRecordService"></param>
        public StudentGrowthRecordController(IStudentGrowthRecordService studentGrowthRecordService)
        {
            _studentGrowthRecordService = studentGrowthRecordService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string studentId = "")
        {
            return Response(success: true, data: await _studentGrowthRecordService.GetPageList(page, limit, studentId));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] StudentGrowthRecordAddViewModel model)
        {
            var result = await _studentGrowthRecordService.Add(model);
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
        public async Task<IActionResult> Put([FromForm] StudentGrowthRecordUpdateViewModel model)
        {
            var result = await _studentGrowthRecordService.Update(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 对一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("single")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _studentGrowthRecordService.GetModel(id));
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
            var result = await _studentGrowthRecordService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 成长记录数量统计
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("studentgrowrecordnumberchartreport")]
        public async Task<IActionResult> StudentGrowRecordNumberChartReport(string designatedMonth)
        {
            var result = await _studentGrowthRecordService.StudentGrowRecordNumberChartReport(designatedMonth);
            return Response(success: true, data: result);
        }
    }
}
