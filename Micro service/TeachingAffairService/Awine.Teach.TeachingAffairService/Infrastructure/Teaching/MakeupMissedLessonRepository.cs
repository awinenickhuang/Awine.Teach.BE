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
    /// 补课管理
    /// </summary>
    public class MakeupMissedLessonRepository : IMakeupMissedLessonRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<MakeupMissedLessonRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public MakeupMissedLessonRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<MakeupMissedLessonRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="status"></param>
        /// <param name="courseId"></param>
        /// <param name="teacherId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MakeupMissedLesson>> GetAll(string tenantId = "", int status = 0, string courseId = "", string teacherId = "", string beginDate = "", string endDate = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_ea_mkeup_missed_lesson WHERE IsDeleted=0 ");
                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }
                if (status > 0)
                {
                    sqlStr.Append(" AND Status=@Status ");
                    parameters.Add("Status", status);
                }
                if (!string.IsNullOrEmpty(courseId))
                {
                    sqlStr.Append(" AND CourseId=@CourseId ");
                    parameters.Add("CourseId", courseId);
                }
                if (!string.IsNullOrEmpty(teacherId))
                {
                    sqlStr.Append(" AND TeacherId=@TeacherId ");
                    parameters.Add("TeacherId", teacherId);
                }
                if (!string.IsNullOrEmpty(beginDate))
                {
                    sqlStr.Append(" AND CourseDates>=@BeginDate ");
                    parameters.Add("@BeginDate", beginDate);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    sqlStr.Append(" AND CourseDates<=@EndDate ");
                    parameters.Add("@EndDate", endDate);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                return await connection.QueryAsync<MakeupMissedLesson>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="status"></param>
        /// <param name="courseId"></param>
        /// <param name="teacherId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPagedList<MakeupMissedLesson>> GetPageList(int page = 1, int limit = 15, string tenantId = "", int status = 0, string courseId = "", string teacherId = "", string beginDate = "", string endDate = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_ea_mkeup_missed_lesson WHERE IsDeleted=0 ");
                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }
                if (status > 0)
                {
                    sqlStr.Append(" AND Status=@Status ");
                    parameters.Add("Status", status);
                }
                if (!string.IsNullOrEmpty(courseId))
                {
                    sqlStr.Append(" AND CourseId=@CourseId ");
                    parameters.Add("CourseId", courseId);
                }
                if (!string.IsNullOrEmpty(teacherId))
                {
                    sqlStr.Append(" AND TeacherId=@TeacherId ");
                    parameters.Add("TeacherId", teacherId);
                }
                if (!string.IsNullOrEmpty(beginDate))
                {
                    sqlStr.Append(" AND CourseDates>=@BeginDate ");
                    parameters.Add("@BeginDate", beginDate);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    sqlStr.Append(" AND CourseDates<=@EndDate ");
                    parameters.Add("@EndDate", endDate);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<MakeupMissedLesson>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MakeupMissedLesson> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_mkeup_missed_lesson WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<MakeupMissedLesson>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Add(MakeupMissedLesson model, CourseSchedule courseSchedule)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //添加补课班级
                        sqlStr.Append(" INSERT INTO t_ea_mkeup_missed_lesson ");
                        sqlStr.Append(" (Id,`Name`,CourseId,CourseName,TeacherId,TeacherName,ClassRoomId,ClassRoom,CourseDates,StartHours,StartMinutes,EndHours,EndMinutes,ClassCourseScheduleId,Status,TenantId,IsDeleted,CreateTime )");
                        sqlStr.Append(" VALUES ");
                        sqlStr.Append(" (@Id,@Name,@CourseId,@CourseName,@TeacherId,@TeacherName,@ClassRoomId,@ClassRoom,@CourseDates,@StartHours,@StartMinutes,@EndHours,@EndMinutes,@ClassCourseScheduleId,@Status,@TenantId,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //添加补课课节 
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO t_ea_course_schedule (Id,Year,ClassId,ClassName,CourseDates,StartHours,StartMinutes,EndHours,EndMinutes,TeacherId,TeacherName, ");
                        sqlStr.Append(" CourseId,CourseName,ClassStatus,ScheduleIdentification,ClassRoomId,ClassRoom,ActualAttendanceNumber,ActualleaveNumber,ActualAbsenceNumber,ConsumedQuantity,ConsumedAmount, ");
                        sqlStr.Append(" TenantId,IsDeleted,CreateTime) ");
                        sqlStr.Append("  VALUES ");
                        sqlStr.Append(" (@Id,@Year,@ClassId,@ClassName,@CourseDates,@StartHours,@StartMinutes,@EndHours,@EndMinutes,@TeacherId,@TeacherName, ");
                        sqlStr.Append(" @CourseId,@CourseName,@ClassStatus,@ScheduleIdentification,@ClassRoomId,@ClassRoom,@ActualAttendanceNumber,@ActualleaveNumber,@ActualAbsenceNumber,@ConsumedQuantity,@ConsumedAmount, ");
                        sqlStr.Append(" @TenantId,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), courseSchedule, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"建班补课时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="courseSchedule"></param>
        /// <returns></returns>
        public async Task<bool> Update(MakeupMissedLesson model, CourseSchedule courseSchedule)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //更新补课班级
                        sqlStr.Append(" UPDATE t_ea_mkeup_missed_lesson SET CourseDates=@CourseDates,StartHours=@StartHours,StartMinutes=@StartMinutes,EndHours=@EndHours,EndMinutes=@EndMinutes,TeacherId=@TeacherId,TeacherName=@TeacherName,CourseId=@CourseId,CourseName=@CourseName,ClassRoomId=@ClassRoomId,ClassRoom=@ClassRoom WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新补课课节 
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_ea_course_schedule SET ClassId=@ClassId,ClassName=@ClassName,CourseDates=@CourseDates,StartHours=@StartHours,StartMinutes=@StartMinutes,EndHours=@EndHours,EndMinutes=@EndMinutes,TeacherId=@TeacherId,TeacherName=@TeacherName,CourseId=@CourseId,CourseName=@CourseName,ClassRoomId=@ClassRoomId,ClassRoom=@ClassRoom WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), courseSchedule, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"更新补课班级时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //删除补课班级
                        sqlStr.Append(" DELETE FROM t_ea_mkeup_missed_lesson WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //删除补课课节 
                        sqlStr.Clear();
                        sqlStr.Append(" DELETE FROM t_ea_course_schedule WHERE ClassId=@ClassId ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { ClassId = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"删除补课班级时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 补课学生分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="makeupMissedLessonId">补课班级ID</param>
        /// <param name="classCourseScheduleId">补课班级课表ID</param>
        /// <returns></returns>
        public async Task<IPagedList<MakeupMissedLessonStudent>> GetMakeupMissedLessonStudentPageList(int page = 1, int limit = 15, string tenantId = "", string makeupMissedLessonId = "", string classCourseScheduleId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT a.Id,a.StudentId,a.AttendanceId,a.ClassCourseScheduleId,a.makeupMissedLessonId,a.TenantId,b.Id,b.StudentId,b.StudentName,b.Gender,b.CourseName,b.ClassesName,b.PurchaseQuantity,b.ConsumedQuantity,b.RemainingNumber,b.ChargeManner,b.CourseDuration,b.TotalPrice,b.UnitPrice,b.LearningProcess FROM t_ea_mkeup_missed_lesson_student AS a INNER JOIN `t_ea_student_course_item` AS b ON a.StudentCourseItemId = b.`Id` WHERE a.IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND a.TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(makeupMissedLessonId))
                {
                    sqlStr.Append(" AND a.MakeupMissedLessonId=@MakeupMissedLessonId ");
                    parameters.Add("MakeupMissedLessonId", makeupMissedLessonId);
                }

                if (!string.IsNullOrEmpty(classCourseScheduleId))
                {
                    sqlStr.Append(" AND a.ClassCourseScheduleId=@ClassCourseScheduleId ");
                    parameters.Add("ClassCourseScheduleId", classCourseScheduleId);
                }

                sqlStr.Append(" ORDER BY a.CreateTime DESC ");

                var list = await connection.QueryAsync<MakeupMissedLessonStudent, StudentCourseItem, MakeupMissedLessonStudent>(sqlStr.ToString(), (mkstudent, courseItem) =>
                {
                    mkstudent.StudentCourseItem = courseItem;
                    return mkstudent;
                },
                parameters,
                splitOn: "Id",
                commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 添加补课学生
        /// </summary>
        /// <param name="students"></param>
        /// <param name="studentAttendances"></param>
        /// <returns></returns>
        public async Task<bool> AddStudentToClass(IList<MakeupMissedLessonStudent> students, IList<StudentAttendance> studentAttendances)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //添加学生
                        sqlStr.Append(" INSERT INTO t_ea_mkeup_missed_lesson_student ");
                        sqlStr.Append(" (Id,`StudentId`,StudentCourseItemId,AttendanceId,ClassCourseScheduleId,MakeupMissedLessonId,TenantId,IsDeleted,CreateTime )");
                        sqlStr.Append(" VALUES ");
                        sqlStr.Append(" (@Id,@StudentId,@StudentCourseItemId,@AttendanceId,@ClassCourseScheduleId,@MakeupMissedLessonId,@TenantId,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), students, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新考勤 
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE `t_ea_students_attendance` SET ProcessingStatus=@ProcessingStatus WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentAttendances, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"添加补课学生时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 移除补课学生
        /// </summary>
        /// <param name="students"></param>
        /// <param name="studentAttendances"></param>
        /// <returns></returns>
        public async Task<bool> RemoveStudentFromClass(IList<MakeupMissedLessonStudent> students, IList<StudentAttendance> studentAttendances)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //移除学生
                        sqlStr.Append(" DELETE FROM t_ea_mkeup_missed_lesson_student WHERE Id=@Id");
                        await connection.ExecuteAsync(sqlStr.ToString(), students, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新出勤 
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE `t_ea_students_attendance` SET ProcessingStatus=@ProcessingStatus WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentAttendances, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"移除补课学生时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
