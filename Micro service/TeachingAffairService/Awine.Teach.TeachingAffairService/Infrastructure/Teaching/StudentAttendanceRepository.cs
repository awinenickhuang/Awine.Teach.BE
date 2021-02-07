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
    /// 学生考勤
    /// </summary>
    public class StudentAttendanceRepository : IStudentAttendanceRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<StudentAttendanceRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public StudentAttendanceRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<StudentAttendanceRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
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
        public async Task<IPagedList<StudentAttendance>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string classId = "", string courseId = "", string studentId = "", string studentName = "", int attendanceStatus = 0, int scheduleIdentification = 0, int processingStatus = 0, string beginDate = "", string endDate = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM t_ea_students_attendance WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("@TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(classId))
                {
                    sqlStr.Append(" AND ClassId=@ClassId ");
                    parameters.Add("@ClassId", classId);
                }

                if (!string.IsNullOrEmpty(courseId))
                {
                    sqlStr.Append(" AND CourseId=@CourseId ");
                    parameters.Add("@CourseId", courseId);
                }

                if (!string.IsNullOrEmpty(studentId))
                {
                    sqlStr.Append(" AND StudentId=@StudentId ");
                    parameters.Add("@StudentId", studentId);
                }

                if (!string.IsNullOrEmpty(studentName))
                {
                    sqlStr.Append(" and StudentName Like @StudentName");
                    parameters.Add("@StudentName", "%" + studentName + "%");
                }

                if (attendanceStatus > 0)
                {
                    sqlStr.Append(" AND AttendanceStatus=@AttendanceStatus ");
                    parameters.Add("@AttendanceStatus", attendanceStatus);
                }

                if (scheduleIdentification > 0)
                {
                    sqlStr.Append(" AND ScheduleIdentification=@ScheduleIdentification ");
                    parameters.Add("@ScheduleIdentification", scheduleIdentification);
                }

                if (processingStatus > 0)
                {
                    sqlStr.Append(" AND ProcessingStatus=@ProcessingStatus ");
                    parameters.Add("@ProcessingStatus", processingStatus);
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

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<StudentAttendance>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StudentAttendance> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_students_attendance WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<StudentAttendance>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 课节点名 -> 出勤情况 -> 正式课节 -> 包括在读学生 跟班试听学生
        /// </summary>
        /// <param name="classCourseSchedule"></param>
        /// <param name="studentCourseItems"></param>
        /// <param name="studentAttendances"></param>
        /// <param name="trialClasses"></param>
        /// <returns></returns>
        public async Task<bool> Attendance(CourseSchedule classCourseSchedule, IList<StudentCourseItem> studentCourseItems, IList<StudentAttendance> studentAttendances, IList<TrialClass> trialClasses)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //更新课节信息
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_ea_course_schedule SET ClassStatus=@ClassStatus,ActualAttendanceNumber=@ActualAttendanceNumber,ActualAbsenceNumber=@ActualAbsenceNumber,ActualleaveNumber=@ActualleaveNumber,ConsumedQuantity=@ConsumedQuantity,ConsumedAmount=@ConsumedAmount WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), classCourseSchedule, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新购买课程 
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_ea_student_course_item SET ConsumedQuantity=@ConsumedQuantity,RemainingNumber=@RemainingNumber WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentCourseItems, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写入考勤记录
                        sqlStr.Clear();
                        sqlStr.Append("INSERT INTO t_ea_students_attendance (Id,`StudentId`,`StudentName`,ClassId,ClassName,CourseId,CourseName,StudentCourseItemId,ClassCourseScheduleId,ScheduleIdentification,AttendanceStatus,RecordStatus,ConsumedQuantity,ProcessingStatus,TenantId,IsDeleted,CreateTime) VALUES (@Id,@StudentId,@StudentName,@ClassId,@ClassName,@CourseId,@CourseName,@StudentCourseItemId,@ClassCourseScheduleId,@ScheduleIdentification,@AttendanceStatus,@RecordStatus,@ConsumedQuantity,@ProcessingStatus,@TenantId,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentAttendances, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新试听学生到课情况
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_market_trial_class SET `ListeningState`=@ListeningState WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), trialClasses, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"正式课节点名时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 课节点名 -> 出勤情况 -> 一对一试听课节
        /// </summary>
        /// <param name="classCourseSchedule">课节息</param>
        /// <param name="trialClasses">试听课节</param>
        /// <returns></returns>
        public async Task<bool> TrialClassSigninAttendance(CourseSchedule classCourseSchedule, IList<TrialClass> trialClasses)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //更新课节信息
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_ea_course_schedule SET ClassStatus=@ClassStatus,ActualAttendanceNumber=@ActualAttendanceNumber,ActualAbsenceNumber=@ActualAbsenceNumber,ActualleaveNumber=@ActualleaveNumber,ConsumedQuantity=@ConsumedQuantity,ConsumedAmount=@ConsumedAmount WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), classCourseSchedule, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新试听学生到课情况
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_market_trial_class SET `ListeningState`=@ListeningState WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), trialClasses, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"一对一试听课节点名时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 课节点名 -> 出勤情况 -> 补课课节
        /// </summary>
        /// <param name="classCourseSchedule">课节息</param>
        /// <param name="studentCourseItems">报读课程</param>
        /// <param name="studentAttendances">出勤信息</param>
        /// <param name="makeupMissedLessonStudentAttendances">出勤信息</param>
        /// <param name="makeupMissedLesson">补课班级</param>
        /// <returns></returns>
        public async Task<bool> MakeupMissedLessonAttendance(CourseSchedule classCourseSchedule, IList<StudentCourseItem> studentCourseItems, IList<StudentAttendance> studentAttendances, IList<StudentAttendance> makeupMissedLessonStudentAttendances, MakeupMissedLesson makeupMissedLesson)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //更新课节信息
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_ea_course_schedule SET ClassStatus=@ClassStatus,ActualAttendanceNumber=@ActualAttendanceNumber,ActualAbsenceNumber=@ActualAbsenceNumber,ActualleaveNumber=@ActualleaveNumber,ConsumedQuantity=@ConsumedQuantity,ConsumedAmount=@ConsumedAmount WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), classCourseSchedule, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新购买课程 
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_ea_student_course_item SET ConsumedQuantity=@ConsumedQuantity,RemainingNumber=@RemainingNumber WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentCourseItems, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写入考勤记录
                        sqlStr.Clear();
                        sqlStr.Append("INSERT INTO t_ea_students_attendance (Id,`StudentId`,`StudentName`,ClassId,ClassName,CourseId,CourseName,StudentCourseItemId,ClassCourseScheduleId,ScheduleIdentification,AttendanceStatus,RecordStatus,ConsumedQuantity,ProcessingStatus,TenantId,IsDeleted,CreateTime) VALUES (@Id,@StudentId,@StudentName,@ClassId,@ClassName,@CourseId,@CourseName,@StudentCourseItemId,@ClassCourseScheduleId,@ScheduleIdentification,@AttendanceStatus,@RecordStatus,@ConsumedQuantity,@ProcessingStatus,@TenantId,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentAttendances, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新补课的考勤记录
                        sqlStr.Clear();
                        sqlStr.Append("UPDATE t_ea_students_attendance SET ProcessingStatus=@ProcessingStatus WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), makeupMissedLessonStudentAttendances, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新补课班级
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_ea_mkeup_missed_lesson SET `Status`=@Status WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), makeupMissedLesson, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"补课课节点名时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
