using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Awine.Teach.TeachingAffairService.Domain;
using Awine.Teach.TeachingAffairService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="studentCourseOrderRepository"></param>
        public StudentCourseOrderService(
            IMapper mapper,
            ILogger<StudentCourseOrderService> logger,
            ICurrentUser user,
            IStudentCourseOrderRepository studentCourseOrderRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _studentCourseOrderRepository = studentCourseOrderRepository;
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
    }
}
