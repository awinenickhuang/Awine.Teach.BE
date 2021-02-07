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
    /// 班级 -> 排课信息 -> 课节信息
    /// </summary>
    public class CourseScheduleRepository : ICourseScheduleRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<CourseScheduleRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public CourseScheduleRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<CourseScheduleRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 排课信息 -> 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="courseDates"></param>
        /// <param name="classStatus">课节状态 1-待上课 2-已结课</param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseSchedule>> GetAll(string tenantId = "", string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", string courseDates = "", int classStatus = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                int year = DateTime.Now.ToLocalTime().Year;

                sqlStr.Append(" SELECT * FROM t_ea_course_schedule WHERE IsDeleted=0 AND Year=@Year ");

                parameters.Add("Year", year);

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(courseId))
                {
                    sqlStr.Append(" AND CourseId=@CourseId ");
                    parameters.Add("CourseId", courseId);
                }

                if (!string.IsNullOrEmpty(classId))
                {
                    sqlStr.Append(" AND ClassId=@ClassId ");
                    parameters.Add("ClassId", classId);
                }

                if (!string.IsNullOrEmpty(teacherId))
                {
                    sqlStr.Append(" AND TeacherId=@TeacherId ");
                    parameters.Add("TeacherId", teacherId);
                }

                if (!string.IsNullOrEmpty(classRoomId))
                {
                    sqlStr.Append(" AND ClassRoomId=@ClassRoomId ");
                    parameters.Add("ClassRoomId", classRoomId);
                }

                if (!string.IsNullOrEmpty(courseDates))
                {
                    sqlStr.Append(" AND CourseDates=@CourseDates ");
                    parameters.Add("CourseDates", courseDates);
                }

                if (classStatus > 0)
                {
                    sqlStr.Append(" AND ClassStatus=@ClassStatus ");
                    parameters.Add("ClassStatus", classStatus);
                }

                sqlStr.Append(" ORDER BY CourseDates ");

                return await connection.QueryAsync<CourseSchedule>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 排课信息 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="classStatus"></param>
        /// <param name="scheduleIdentification"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPagedList<CourseSchedule>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", int classStatus = 0, int scheduleIdentification = 0, string beginDate = "", string endDate = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                int year = DateTime.Now.ToLocalTime().Year;

                sqlStr.Append(" SELECT * FROM t_ea_course_schedule WHERE IsDeleted=0 AND Year=@Year ");

                parameters.Add("Year", year);

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(courseId))
                {
                    sqlStr.Append(" AND CourseId=@CourseId ");
                    parameters.Add("CourseId", courseId);
                }

                if (!string.IsNullOrEmpty(classId))
                {
                    sqlStr.Append(" AND ClassId=@ClassId ");
                    parameters.Add("ClassId", classId);
                }

                if (!string.IsNullOrEmpty(teacherId))
                {
                    sqlStr.Append(" AND TeacherId=@TeacherId ");
                    parameters.Add("TeacherId", teacherId);
                }

                if (!string.IsNullOrEmpty(classRoomId))
                {
                    sqlStr.Append(" AND ClassRoomId=@ClassRoomId ");
                    parameters.Add("ClassRoomId", classRoomId);
                }

                if (classStatus > 0)
                {
                    sqlStr.Append(" AND ClassStatus=@ClassStatus ");
                    parameters.Add("ClassStatus", classStatus);
                }

                if (scheduleIdentification > 0)
                {
                    sqlStr.Append(" AND ScheduleIdentification=@ScheduleIdentification ");
                    parameters.Add("ScheduleIdentification", scheduleIdentification);
                }

                if (!string.IsNullOrEmpty(beginDate))
                {
                    sqlStr.Append(" AND CreateTime>=@BeginDate ");
                    parameters.Add("@BeginDate", beginDate);
                }

                if (!string.IsNullOrEmpty(endDate))
                {
                    sqlStr.Append(" AND CreateTime<=@EndDate ");
                    parameters.Add("@EndDate", endDate);
                }

                sqlStr.Append(" ORDER BY CourseDates ");

                var list = await connection.QueryAsync<CourseSchedule>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CourseSchedule> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_course_schedule WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<CourseSchedule>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加排课计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddClassSchedulingPlans(IList<CourseSchedule> models)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" INSERT INTO t_ea_course_schedule (Id,Year,ClassId,ClassName,CourseDates,StartHours,StartMinutes,EndHours,EndMinutes,TeacherId,TeacherName, ");
                sqlStr.Append(" CourseId,CourseName,ClassStatus,ScheduleIdentification,ClassRoomId,ClassRoom,ActualAttendanceNumber,ActualleaveNumber,ActualAbsenceNumber,ConsumedQuantity,ConsumedAmount, ");
                sqlStr.Append(" TenantId,IsDeleted,CreateTime) ");
                sqlStr.Append("  VALUES ");
                sqlStr.Append(" (@Id,@Year,@ClassId,@ClassName,@CourseDates,@StartHours,@StartMinutes,@EndHours,@EndMinutes,@TeacherId,@TeacherName, ");
                sqlStr.Append(" @CourseId,@CourseName,@ClassStatus,@ScheduleIdentification,@ClassRoomId,@ClassRoom,@ActualAttendanceNumber,@ActualleaveNumber,@ActualAbsenceNumber,@ConsumedQuantity,@ConsumedAmount, ");
                sqlStr.Append(" @TenantId,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), models, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(CourseSchedule model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE t_ea_course_schedule SET ClassId=@ClassId,ClassName=@ClassName,CourseDates=@CourseDates,StartHours=@StartHours,StartMinutes=@StartMinutes,EndHours=@EndHours,EndMinutes=@EndMinutes,TeacherId=@TeacherId,TeacherName=@TeacherName,CourseId=@CourseId,CourseName=@CourseName,ClassRoomId=@ClassRoomId,ClassRoom=@ClassRoom WHERE Id=@Id ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                string sqlStr = "DELETE FROM t_ea_course_schedule WHERE Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
