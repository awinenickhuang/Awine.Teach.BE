using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public interface IStudentCourseOrderRepository
    {
        /// <summary>
        /// 学生所有订单数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<IEnumerable<StudentCourseOrder>> GetAll(string tenantId, string studentId);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="salesStaffId"></param>
        /// <param name="marketingChannelId"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        Task<IPagedList<StudentCourseOrder>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string studentId = "", string courseId = "", string salesStaffId = "", string marketingChannelId = "", string beginDate = "", string finishDate = "");
    }
}
