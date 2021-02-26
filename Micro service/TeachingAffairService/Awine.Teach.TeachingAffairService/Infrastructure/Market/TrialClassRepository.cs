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
    /// 试听管理
    /// </summary>
    public class TrialClassRepository : ITrialClassRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<TrialClassRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public TrialClassRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<TrialClassRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="listeningState"></param>
        /// <param name="courseScheduleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TrialClass>> GetAll(string tenantId = "", int listeningState = 0, string courseScheduleId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_market_trial_class WHERE IsDeleted=0 ");
                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }
                if (listeningState > 0)
                {
                    sqlStr.Append(" AND ListeningState=@ListeningState ");
                    parameters.Add("ListeningState", listeningState);
                }
                if (!string.IsNullOrEmpty(courseScheduleId))
                {
                    sqlStr.Append(" AND CourseScheduleId=@CourseScheduleId ");
                    parameters.Add("CourseScheduleId", courseScheduleId);
                }
                sqlStr.Append(" ORDER BY CreateTime DESC ");

                return await connection.QueryAsync<TrialClass>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="listeningState"></param>
        /// <param name="teacherId"></param>
        /// <param name="courseId"></param>
        /// <param name="courseScheduleId"></param>
        /// <returns></returns>
        public async Task<IPagedList<TrialClass>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string name = "", string phoneNumber = "", int listeningState = 0, string teacherId = "", string courseId = "", string courseScheduleId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_market_trial_class WHERE IsDeleted=0 ");
                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    sqlStr.Append(" and Name Like @Name");
                    parameters.Add("@Name", "%" + name + "%");
                }
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    sqlStr.Append(" AND PhoneNumber=@PhoneNumber ");
                    parameters.Add("PhoneNumber", phoneNumber);
                }
                if (listeningState > 0)
                {
                    sqlStr.Append(" AND ListeningState=@ListeningState ");
                    parameters.Add("ListeningState", listeningState);
                }
                if (!string.IsNullOrEmpty(teacherId))
                {
                    sqlStr.Append(" AND TeacherId=@TeacherId ");
                    parameters.Add("TeacherId", teacherId);
                }
                if (!string.IsNullOrEmpty(courseId))
                {
                    sqlStr.Append(" AND CourseId=@CourseId ");
                    parameters.Add("CourseId", courseId);
                }
                if (!string.IsNullOrEmpty(courseScheduleId))
                {
                    sqlStr.Append(" AND CourseScheduleId=@CourseScheduleId ");
                    parameters.Add("CourseScheduleId", courseScheduleId);
                }
                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<TrialClass>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TrialClass> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_market_trial_class WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<TrialClass>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TrialClass> GetModel(TrialClass model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_market_trial_class WHERE StudentId=@StudentId AND TenantId=@TenantId AND CourseScheduleId=@CourseScheduleId AND ListeningState=@ListeningState ");
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }
                return await connection.QueryFirstOrDefaultAsync<TrialClass>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 跟班试听
        /// </summary>
        /// <param name="trialClass"></param>
        /// <returns></returns>
        public async Task<int> ListenFollowClass(TrialClass trialClass)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" INSERT INTO t_market_trial_class ");
                sqlStr.Append(" (Id,StudentId,`StudentName`,Gender,`PhoneNumber`,TeacherId,TeacherName,CourseId,CourseName,CourseScheduleId,CourseScheduleInformation,IsTransformation,ListeningState,TrialClassGenre,StudentCategory,CreatorId,CreatorName,TenantId,IsDeleted,CreateTime) ");
                sqlStr.Append(" VALUES");
                sqlStr.Append(" (@Id,@StudentId,@StudentName,@Gender,@PhoneNumber,@TeacherId,@TeacherName,@CourseId,@CourseName,@CourseScheduleId,@CourseScheduleInformation,@IsTransformation,@ListeningState,@TrialClassGenre,@StudentCategory,@CreatorId,@CreatorName,@TenantId,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), trialClass, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 一对一试听
        /// </summary>
        /// <param name="trialClass"></param>
        /// <param name="courseSchedules"></param>
        /// <returns></returns>
        public async Task<bool> OneToOneListen(TrialClass trialClass, CourseSchedule courseSchedules)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //写试听信息
                        sqlStr.Append(" INSERT INTO t_market_trial_class ");
                        sqlStr.Append(" (Id,StudentId,`StudentName`,Gender,`PhoneNumber`,TeacherId,TeacherName,CourseId,CourseName,CourseScheduleId,CourseScheduleInformation,IsTransformation,ListeningState,TrialClassGenre,StudentCategory,CreatorId,CreatorName,TenantId,IsDeleted,CreateTime) ");
                        sqlStr.Append(" VALUES");
                        sqlStr.Append(" (@Id,@StudentId,@StudentName,@Gender,@PhoneNumber,@TeacherId,@TeacherName,@CourseId,@CourseName,@CourseScheduleId,@CourseScheduleInformation,@IsTransformation,@ListeningState,@TrialClassGenre,@StudentCategory,@CreatorId,@CreatorName,@TenantId,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), trialClass, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写课节信息 - 试听课
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO t_ea_course_schedule (Id,Year,ClassId,ClassName,CourseDates,StartHours,StartMinutes,EndHours,EndMinutes,TeacherId,TeacherName, ");
                        sqlStr.Append(" CourseId,CourseName,ClassStatus,ScheduleIdentification,ClassRoomId,ClassRoom,ActualAttendanceNumber,ActualleaveNumber,ActualAbsenceNumber,ConsumedQuantity,ConsumedAmount, ");
                        sqlStr.Append(" TenantId,IsDeleted,CreateTime) ");
                        sqlStr.Append("  VALUES ");
                        sqlStr.Append(" (@Id,@Year,@ClassId,@ClassName,@CourseDates,@StartHours,@StartMinutes,@EndHours,@EndMinutes,@TeacherId,@TeacherName, ");
                        sqlStr.Append(" @CourseId,@CourseName,@ClassStatus,@ScheduleIdentification,@ClassRoomId,@ClassRoom,@ActualAttendanceNumber,@ActualleaveNumber,@ActualAbsenceNumber,@ConsumedQuantity,@ConsumedAmount, ");
                        sqlStr.Append(" @TenantId,@IsDeleted,@CreateTime) ");

                        await connection.ExecuteAsync(sqlStr.ToString(), courseSchedules, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"创建一对一试听时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 更新 -> 到课状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateListeningState(TrialClass model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //更新到课状态
                        sqlStr.Append(" UPDATE t_market_trial_class SET `ListeningState`=@ListeningState WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //如果为到课状态 则更新咨询记录中对应学生的跟进状态为 -> 已试听
                        if (model.ListeningState == 2)
                        {
                            sqlStr.Clear();
                            sqlStr.Append(" UPDATE t_market_consult_record SET TrackingState=@TrackingState WHERE Id=@Id ");
                            await connection.ExecuteAsync(sqlStr.ToString(), new { TrackingState = 4, Id = model.StudentId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"更新 -> 到课状态时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Delete(TrialClass model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //删除试听信息
                        sqlStr.Append(" DELETE FROM t_market_trial_class WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { Id = model.Id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //删除课节信息
                        sqlStr.Clear();
                        sqlStr.Append(" DELETE FROM t_ea_course_schedule WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { Id = model.CourseScheduleId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"删除听时信息时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
