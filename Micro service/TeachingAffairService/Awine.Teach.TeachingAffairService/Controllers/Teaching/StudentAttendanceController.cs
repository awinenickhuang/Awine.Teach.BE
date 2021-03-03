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
    /// 学生考勤
    /// </summary>
    public class StudentAttendanceController : ApiController
    {
        /// <summary>
        /// IStudentAttendanceService
        /// </summary>
        private readonly IStudentAttendanceService _studentAttendanceService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="studentAttendanceService"></param>
        public StudentAttendanceController(IStudentAttendanceService studentAttendanceService)
        {
            _studentAttendanceService = studentAttendanceService;
        }

        /// <summary>
        /// 所有考勤记录 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="classId"></param>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <param name="studentName"></param>
        /// <param name="attendanceStatus"></param>
        /// <param name="scheduleIdentification"></param>
        /// <param name="processingStatus"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string classId = "", string courseId = "", string studentId = "", string studentName = "", int attendanceStatus = 0, int scheduleIdentification = 0, int processingStatus = 0, string beginDate = "", string endDate = "")
        {
            return Response(success: true, data: await _studentAttendanceService.GetPageList(page, limit, classId, courseId, studentId, studentName, attendanceStatus, scheduleIdentification, processingStatus, beginDate, endDate));
        }

        /// <summary>
        /// 课节点名 -> 正式课节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("attendance")]
        public async Task<IActionResult> Attendance([FromForm] SignInViewModel model)
        {
            var result = await _studentAttendanceService.Attendance(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 课节点名 -> 一对一试听课节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("trialclassattendance")]
        public async Task<IActionResult> TrialClassSigninAttendance([FromForm] SignInViewModel model)
        {
            var result = await _studentAttendanceService.TrialClassSigninAttendance(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 课节点名 -> 补课课节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("makeupmissedlessonattendance")]
        public async Task<IActionResult> MakeupMissedLessonAttendance([FromForm] MakeupMissedLessonAttendanceViewModel model)
        {
            var result = await _studentAttendanceService.MakeupMissedLessonAttendance(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 取消考勤
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("cancel")]
        public async Task<IActionResult> CancelAttendance([FromForm] string id)
        {
            var result = await _studentAttendanceService.CancelAttendance(id);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 课消金额统计分析
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("attendanceamountreport")]
        public async Task<IActionResult> AttendanceAmountReport(string date)
        {
            var result = await _studentAttendanceService.AttendanceAmountReport(date);

            return Response(success: true, data: result);
        }

        /// <summary>
        /// 课消数量统计分析
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("attendancenumberreport")]
        public async Task<IActionResult> AttendanceNumberReport(string date)
        {
            var result = await _studentAttendanceService.AttendanceNumberReport(date);

            return Response(success: true, data: result);
        }
    }
}