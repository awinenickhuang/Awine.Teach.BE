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
    /// 补课管理
    /// </summary>
    public class MakeupMissedLessonController : ApiController
    {
        /// <summary>
        /// IMakeupMissedLessonService
        /// </summary>
        private readonly IMakeupMissedLessonService _makeupMissedLessonService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="makeupMissedLessonService"></param>
        public MakeupMissedLessonController(IMakeupMissedLessonService makeupMissedLessonService)
        {
            _makeupMissedLessonService = makeupMissedLessonService;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="status"></param>
        /// <param name="courseId"></param>
        /// <param name="teacherId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(int status = 0, string courseId = "", string teacherId = "", string beginDate = "", string endDate = "")
        {
            return Response(success: true, data: await _makeupMissedLessonService.GetAll(status, courseId, teacherId, beginDate, endDate));
        }

        /// <summary>
        /// 分页列表 -> 一般用于表格
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="status"></param>
        /// <param name="courseId"></param>
        /// <param name="teacherId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, int status = 0, string courseId = "", string teacherId = "", string beginDate = "", string endDate = "")
        {
            return Response(success: true, data: await _makeupMissedLessonService.GetPageList(page, limit, status, courseId, teacherId, beginDate, endDate));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]MakeupMissedLessonAddViewModel model)
        {
            var result = await _makeupMissedLessonService.Add(model);
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
        public async Task<IActionResult> Put([FromForm]MakeupMissedLessonUpdateViewModel model)
        {
            var result = await _makeupMissedLessonService.Update(model);
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
            var result = await _makeupMissedLessonService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }


        /// <summary>
        /// 分页列表：查询补课班级中的学生信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="makeupMissedLessonId">补课班级ID</param>
        /// <param name="classCourseScheduleId">补课班级课表ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("mkspagelist")]
        public async Task<IActionResult> GetMakeupMissedLessonStudentPageList(int page = 1, int limit = 15, string makeupMissedLessonId = "", string classCourseScheduleId = "")
        {
            return Response(success: true, data: await _makeupMissedLessonService.GetMakeupMissedLessonStudentPageList(page, limit, makeupMissedLessonId, classCourseScheduleId));
        }

        /// <summary>
        /// 添加补课学生
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addstudenttoclass")]
        public async Task<IActionResult> AddStudentToClass([FromForm]MakeupMissedLessonStudentAddViewModel model)
        {
            var result = await _makeupMissedLessonService.AddStudentToClass(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 添加补课学生
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("removestudentfromclass")]
        public async Task<IActionResult> RemoveStudentFromClass([FromForm]MakeupMissedLessonStudentRemoveViewModel model)
        {
            var result = await _makeupMissedLessonService.RemoveStudentFromClass(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}