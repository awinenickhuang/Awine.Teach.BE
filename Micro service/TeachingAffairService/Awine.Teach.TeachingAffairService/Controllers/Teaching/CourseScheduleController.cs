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
    /// 班级 -> 排课信息
    /// </summary>
    public class CourseScheduleController : ApiController
    {
        /// <summary>
        /// ICourseScheduleService
        /// </summary>
        private readonly ICourseScheduleService _courseScheduleService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="courseScheduleService"></param>
        public CourseScheduleController(ICourseScheduleService courseScheduleService)
        {
            _courseScheduleService = courseScheduleService;
        }

        /// <summary>
        /// 排课信息 -> 所有数据 -> 用于初始化课表日历 -> 携带日历组件需要的信息
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="courseDates"></param>
        /// <param name="classStatus"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", string courseDates = "", int classStatus = 0)
        {
            return Response(success: true, data: await _courseScheduleService.GetAll(courseId, classId, teacherId, classRoomId, courseDates, classStatus));
        }

        /// <summary>
        /// 排课信息 -> 所有数据 -> 用于查询课程明细
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="courseDates"></param>
        /// <param name="classStatus"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("listschedule")]
        public async Task<IActionResult> GetAllSchedule(string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", string courseDates = "", int classStatus = 0)
        {
            return Response(success: true, data: await _courseScheduleService.GetAllSchedule(courseId, classId, teacherId, classRoomId, courseDates, classStatus));
        }

        /// <summary>
        /// 排课信息 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="classStatus"></param>
        /// <param name="scheduleIdentification"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", int classStatus = 0, int scheduleIdentification = 0, string beginDate = "", string endDate = "")
        {
            return Response(success: true, data: await _courseScheduleService.GetPageList(page, limit, courseId, classId, teacherId, classRoomId, classStatus, scheduleIdentification, beginDate, endDate));
        }

        /// <summary>
        /// 生成排课计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addclassschedulingplan")]
        public async Task<IActionResult> AddClassSchedulingPlans([FromForm]CourseScheduleAddViewModel model)
        {
            var result = await _courseScheduleService.AddClassSchedulingPlans(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 课节调整
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromForm]CourseScheduleUpdateViewModel model)
        {
            var result = await _courseScheduleService.Update(model);

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
            var result = await _courseScheduleService.Delete(id);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}