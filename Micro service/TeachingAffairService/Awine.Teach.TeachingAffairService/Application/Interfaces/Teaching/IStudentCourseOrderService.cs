using Awine.Framework.AspNetCore.Model;
using Awine.Framework.Core.Collections;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Interfaces
{
    /// <summary>
    /// 学生订单
    /// </summary>
    public interface IStudentCourseOrderService
    {
        /// <summary>
        /// 学生所有订单数据
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<IEnumerable<StudentCourseOrderViewModel>> GetAll(string studentId);

        /// <summary>
        /// 分页列表
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
        Task<IPagedList<StudentCourseOrderViewModel>> GetPageList(int page = 1, int limit = 15, string studentId = "", string courseId = "", string salesStaffId = "", string marketingChannelId = "", string beginDate = "", string finishDate = "");

        /// <summary>
        /// 订单情况统计 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 指定月份的订单数量
        /// </remarks>
        Task<BasicLineChartViewModel> OrderMonthChartReport(string designatedMonth);

        /// <summary>
        /// 课程订单情况统计 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 指定月份的课程订单数量
        /// </remarks>
        Task<StackedLineChartViewModel> CourseOrderMonthChartReport(string designatedMonth);
    }
}
