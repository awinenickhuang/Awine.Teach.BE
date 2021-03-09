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
    /// 学生选课信息
    /// </summary>
    public class StudentCourseItemRepository : IStudentCourseItemRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<StudentCourseItemRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public StudentCourseItemRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<StudentCourseItemRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 所有数据 -> 不分页列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="classesId"></param>
        /// <param name="studentOrderId"></param>
        /// <param name="learningProcess"></param>
        /// <param name="chargeManner"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StudentCourseItem>> GetAll(string tenantId = "", string studentId = "", string courseId = "", string classesId = "", string studentOrderId = "", int learningProcess = 0, int chargeManner = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_student_course_item WHERE IsDeleted=0 ");
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

                if (!string.IsNullOrEmpty(classesId))
                {
                    sqlStr.Append(" AND ClassesId=@ClassesId ");
                    parameters.Add("ClassesId", classesId);
                }

                if (!string.IsNullOrEmpty(studentOrderId))
                {
                    sqlStr.Append(" AND StudentOrderId=@StudentOrderId ");
                    parameters.Add("StudentOrderId", studentOrderId);
                }

                if (learningProcess > 0)
                {
                    sqlStr.Append(" AND LearningProcess=@LearningProcess ");
                    parameters.Add("LearningProcess", learningProcess);
                }

                if (chargeManner > 0)
                {
                    sqlStr.Append(" AND ChargeManner=@ChargeManner ");
                    parameters.Add("ChargeManner", chargeManner);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");
                return await connection.QueryAsync<StudentCourseItem>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 所有数据 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="classesId"></param>
        /// <param name="studentOrderId"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        public async Task<IPagedList<StudentCourseItem>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string studentId = "", string courseId = "", string classesId = "", string studentOrderId = "", int learningProcess = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM t_ea_student_course_item WHERE IsDeleted=0 ");

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

                if (!string.IsNullOrEmpty(classesId))
                {
                    sqlStr.Append(" AND ClassesId=@ClassesId ");
                    parameters.Add("ClassesId", classesId);
                }

                if (!string.IsNullOrEmpty(studentOrderId))
                {
                    sqlStr.Append(" AND StudentOrderId=@StudentOrderId ");
                    parameters.Add("StudentOrderId", studentOrderId);
                }

                if (learningProcess > 0)
                {
                    sqlStr.Append(" AND LearningProcess=@LearningProcess ");
                    parameters.Add("LearningProcess", learningProcess);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<StudentCourseItem>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 更新学生的就读班级信息 -> 从班级添加或移除学生
        /// </summary>
        /// <param name="model"></param>
        /// <param name="clssses"></param>
        /// <returns></returns>
        public async Task<bool> UpdateStudentsClassInformation(List<StudentCourseItem> model, Classes clssses)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //更新学生报读课程对应的班级信息
                        sqlStr.Append(" UPDATE t_ea_student_course_item SET `ClassesId`=@ClassesId,ClassesName=@ClassesName,LearningProcess=@LearningProcess WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新班级招生进度信息
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_ea_classes SET `OwnedStudents`=@OwnedStudents WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), clssses, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"更新学生的就读班级信息-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StudentCourseItem> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_student_course_item WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<StudentCourseItem>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        ///// <summary>
        ///// 取一条数据
        ///// </summary>
        ///// <param name="tenantId"></param>
        ///// <param name="studentId"></param>
        ///// <param name="courseId"></param>
        ///// <param name="chargeManner"></param>
        ///// <param name="learningProcess"></param>
        ///// <returns></returns>
        //public async Task<StudentCourseItem> GetModel(string tenantId, string studentId, string courseId, int chargeManner, int learningProcess)
        //{
        //    using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
        //    {
        //        StringBuilder sqlStr = new StringBuilder();
        //        sqlStr.Append(" SELECT * FROM t_ea_student_course_item WHERE TenantId=@TenantId AND StudentId=@StudentId AND CourseId=@CourseId AND ChargeManner=@ChargeManner AND LearningProcess=@LearningProcess AND IsDeleted=0");
        //        return await connection.QueryFirstOrDefaultAsync<StudentCourseItem>(sqlStr.ToString(), new { TenantId = tenantId, StudentId = studentId, CourseId = courseId, ChargeManner = chargeManner, LearningProcess = learningProcess }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
        //    }
        //}

        /// <summary>
        /// 更新报读课程的学习进度 -> 学习进度 1-已报名（未分班） 2-已报名（已分班）3-停课 4-退费 5-毕业
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> UpdateLearningProcess(StudentCourseItem model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE t_ea_student_course_item SET LearningProcess=@LearningProcess WHERE Id=@Id ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
