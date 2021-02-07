using Awine.Framework.Core.Collections;
using Awine.Framework.Dapper.Extensions.Options;
using Awine.Teach.TeachingAffairService.Domain;
using Awine.Teach.TeachingAffairService.Domain.Interface;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Infrastructure.Repository
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public class StudentCourseOrderRepository : IStudentCourseOrderRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<StudentCourseOrderRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public StudentCourseOrderRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<StudentCourseOrderRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 学生所有订单数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StudentCourseOrder>> GetAll(string tenantId, string studentId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_student_course_order WHERE IsDeleted=0 AND TenantId=@TenantId AND StudentId=@StudentId ORDER BY CreateTime DESC ");
                return await connection.QueryAsync<StudentCourseOrder>(sqlStr.ToString(), new { TenantId = tenantId, StudentId = studentId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

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
        public async Task<IPagedList<StudentCourseOrder>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string studentId = "", string courseId = "", string salesStaffId = "", string marketingChannelId = "", string beginDate = "", string finishDate = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_ea_student_course_order WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(studentId))
                {
                    sqlStr.Append(" AND StudentId=@StudentId ");
                    parameters.Add("StudentId", studentId);
                }

                if (!string.IsNullOrEmpty(courseId))
                {
                    sqlStr.Append(" AND CourseId=@CourseId ");
                    parameters.Add("CourseId", courseId);
                }

                if (!string.IsNullOrEmpty(salesStaffId))
                {
                    sqlStr.Append(" AND SalesStaffId=@SalesStaffId ");
                    parameters.Add("SalesStaffId", salesStaffId);
                }

                if (!string.IsNullOrEmpty(marketingChannelId))
                {
                    sqlStr.Append(" AND MarketingChannelId=@MarketingChannelId ");
                    parameters.Add("MarketingChannelId", marketingChannelId);
                }

                if (!string.IsNullOrEmpty(beginDate))
                {
                    sqlStr.Append(" AND CreateTime>=@BeginDate ");
                    parameters.Add("BeginDate", beginDate);
                }

                if (!string.IsNullOrEmpty(finishDate))
                {
                    sqlStr.Append(" AND CreateTime<=@FinishDate ");
                    parameters.Add("FinishDate", finishDate);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<StudentCourseOrder>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }
    }
}
