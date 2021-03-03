using AutoMapper;
using Awine.Framework.AspNetCore.Model;
using Awine.Framework.Core;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Awine.Teach.TeachingAffairService.Domain;
using Awine.Teach.TeachingAffairService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Services
{
    /// <summary>
    /// 学生订单
    /// </summary>
    public class StudentCourseOrderService : IStudentCourseOrderService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<StudentCourseOrderService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// IStudentCourseOrderRepository
        /// </summary>
        private readonly IStudentCourseOrderRepository _studentCourseOrderRepository;

        /// <summary>
        /// ICourseRepository
        /// </summary>
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="studentCourseOrderRepository"></param>
        /// <param name="courseRepository"></param>
        public StudentCourseOrderService(
            IMapper mapper,
            ILogger<StudentCourseOrderService> logger,
            ICurrentUser user,
            IStudentCourseOrderRepository studentCourseOrderRepository,
            ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _studentCourseOrderRepository = studentCourseOrderRepository;
            _courseRepository = courseRepository;
        }

        /// <summary>
        /// 学生所有订单数据
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StudentCourseOrderViewModel>> GetAll(string studentId)
        {
            var entities = await _studentCourseOrderRepository.GetAll(_user.TenantId, studentId);

            return _mapper.Map<IEnumerable<StudentCourseOrder>, IEnumerable<StudentCourseOrderViewModel>>(entities);
        }

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
        public async Task<IPagedList<StudentCourseOrderViewModel>> GetPageList(int page = 1, int limit = 15, string studentId = "", string courseId = "", string salesStaffId = "", string marketingChannelId = "", string beginDate = "", string finishDate = "")
        {
            var entities = await _studentCourseOrderRepository.GetPageList(page, limit, _user.TenantId, studentId, courseId, salesStaffId, marketingChannelId, beginDate, finishDate);

            return _mapper.Map<IPagedList<StudentCourseOrder>, IPagedList<StudentCourseOrderViewModel>>(entities);
        }

        #region 数据统计分析

        /// <summary>
        /// 订单情况统计 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 指定月份的订单数量
        /// </remarks>
        public async Task<BasicLineChartViewModel> OrderMonthChartReport(string designatedMonth)
        {
            var orders = await _studentCourseOrderRepository.GetAll(tenantId: _user.TenantId);

            DateTime time = Convert.ToDateTime(designatedMonth);

            var dateFrom = TimeCalculate.FirstDayOfMonth(time);
            orders = orders.Where(x => x.CreateTime >= dateFrom);

            var dateTo = TimeCalculate.LastDayOfMonth(time);
            orders = orders.Where(x => x.CreateTime <= dateTo);

            //当前月天数
            int days = TimeCalculate.DaysInMonth(time);

            BasicLineChartViewModel basicLineChartViewModel = new BasicLineChartViewModel();

            for (int day = 1; day <= days; day++)
            {
                basicLineChartViewModel.XAxisData.Add($"{time.Year}-{time.Month}-{day}");
                basicLineChartViewModel.SeriesData.Add(orders.Where(x => x.CreateTime.Year == time.Year
                && x.CreateTime.Month == time.Month && x.CreateTime.Day == day).Count());
            }

            return basicLineChartViewModel;
        }

        /// <summary>
        /// 课程订单情况统计 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 指定月份的课程订单数量
        /// </remarks>
        public async Task<StackedLineChartViewModel> CourseOrderMonthChartReport(string designatedMonth)
        {
            var orders = await _studentCourseOrderRepository.GetAll(tenantId: _user.TenantId);

            DateTime time = Convert.ToDateTime(designatedMonth);

            var dateFrom = TimeCalculate.FirstDayOfMonth(time);
            orders = orders.Where(x => x.CreateTime >= dateFrom);

            var dateTo = TimeCalculate.LastDayOfMonth(time);
            orders = orders.Where(x => x.CreateTime <= dateTo);

            //当前月天数
            int days = TimeCalculate.DaysInMonth(time);

            StackedLineChartViewModel stackedLineChartViewModel = new StackedLineChartViewModel();

            //取所有状态为启用的课程
            var courses = await _courseRepository.GetAll(tenantId: _user.TenantId, enabledStatus: 1);

            foreach (var course in courses)
            {
                stackedLineChartViewModel.Legend.Data.Add(course.Name);

                stackedLineChartViewModel.Series.Add(new StackedLineChartSeries()
                {
                    Id = course.Id,
                    Name = course.Name
                });
            }

            for (int day = 1; day <= days; day++)
            {
                stackedLineChartViewModel.XAxis.Data.Add($"{time.Year}-{time.Month}-{day}");

                foreach (var ser in stackedLineChartViewModel.Series)
                {
                    //填充 图表 数据
                    ser.Data.Add(orders.Where(x => x.CourseId.Equals(ser.Id) && x.CreateTime.Year == time.Year && x.CreateTime.Month == time.Month && x.CreateTime.Day == day).Count());
                }
            }

            return stackedLineChartViewModel;
        }

        #endregion
    }
}
