using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.TeachingAffairService.Controllers
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public class StudentCourseOrderController : ApiController
    {
        /// <summary>
        /// 订单管理
        /// </summary>
        private readonly IStudentCourseOrderService _studentCourseOrderService;

        /// <summary>
        /// 构造函数 - 注入需要的资源
        /// </summary>
        /// <param name="studentCourseOrderService"></param>
        public StudentCourseOrderController(IStudentCourseOrderService studentCourseOrderService)
        {
            _studentCourseOrderService = studentCourseOrderService;
        }

        /// <summary>
        /// 学生所有订单数据
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(string studentId)
        {
            return Response(success: true, data: await _studentCourseOrderService.GetAll(studentId));
        }

        /// <summary>
        /// 订单分页列表数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="salesStaffId"></param>
        /// <param name="marketingChannelId"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string studentId = "", string courseId = "", string salesStaffId = "", string marketingChannelId = "", string beginDate = "", string finishDate = "")
        {
            return Response(success: true, data: await _studentCourseOrderService.GetPageList(page, limit, studentId, courseId, salesStaffId, marketingChannelId, beginDate, finishDate));
        }

        /// <summary>
        /// 订单情况统计 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 指定月份的订单数量
        /// </remarks>
        [HttpGet]
        [Route("ordermonthcahrtreport")]
        public async Task<IActionResult> OrderMonthChartReport(string designatedMonth)
        {
            var chartData = await _studentCourseOrderService.OrderMonthChartReport(designatedMonth);
            return Response(success: true, data: chartData);
        }

        /// <summary>
        /// 课程订单情况统计 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 指定月份的课程订单数量
        /// </remarks>
        [HttpGet]
        [Route("courseordermonthcahrtreport")]
        public async Task<IActionResult> CourseOrderMonthChartReport(string designatedMonth)
        {
            var chartData = await _studentCourseOrderService.CourseOrderMonthChartReport(designatedMonth);
            return Response(success: true, data: chartData);
        }
    }
}