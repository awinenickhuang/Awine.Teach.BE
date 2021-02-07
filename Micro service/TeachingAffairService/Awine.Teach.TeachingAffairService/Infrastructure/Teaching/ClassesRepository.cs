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
    /// 班级管理
    /// </summary>
    public class ClassesRepository : IClassesRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<ClassesRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public ClassesRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<ClassesRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="courseId"></param>
        /// <param name="recruitStatus"></param>
        /// <param name="typeOfClass"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Classes>> GetAll(string tenantId = "", string courseId = "", int recruitStatus = 0, int typeOfClass = 0, string beginDate = "", string finishDate = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_classes WHERE IsDeleted=0 ");
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
                if (recruitStatus > 0)
                {
                    sqlStr.Append(" AND RecruitStatus=@RecruitStatus ");
                    parameters.Add("RecruitStatus", recruitStatus);
                }
                if (typeOfClass > 0)
                {
                    sqlStr.Append(" AND TypeOfClass=@TypeOfClass ");
                    parameters.Add("TypeOfClass", typeOfClass);
                }
                if (!string.IsNullOrEmpty(beginDate))
                {
                    sqlStr.Append(" AND StartDate>=@BeginDate ");
                    parameters.Add("BeginDate", beginDate);
                }
                if (!string.IsNullOrEmpty(finishDate))
                {
                    sqlStr.Append(" AND StartDate<=@FinishDate ");
                    parameters.Add("FinishDate", finishDate);
                }
                sqlStr.Append(" ORDER BY StartDate ");
                return await connection.QueryAsync<Classes>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="name"></param>
        /// <param name="courseId"></param>
        /// <param name="recruitStatus"></param>
        /// <param name="typeOfClass"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        public async Task<IPagedList<Classes>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string name = "", string courseId = "", int recruitStatus = 0, int typeOfClass = 0, string beginDate = "", string finishDate = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM t_ea_classes WHERE IsDeleted=0 ");
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
                if (!string.IsNullOrEmpty(courseId))
                {
                    sqlStr.Append(" AND CourseId=@CourseId ");
                    parameters.Add("CourseId", courseId);
                }
                if (recruitStatus > 0)
                {
                    sqlStr.Append(" AND RecruitStatus=@RecruitStatus ");
                    parameters.Add("RecruitStatus", recruitStatus);
                }
                if (typeOfClass > 0)
                {
                    sqlStr.Append(" AND TypeOfClass=@TypeOfClass ");
                    parameters.Add("TypeOfClass", typeOfClass);
                }
                if (!string.IsNullOrEmpty(beginDate))
                {
                    sqlStr.Append(" AND StartDate>=@BeginDate ");
                    parameters.Add("BeginDate", beginDate);
                }
                if (!string.IsNullOrEmpty(finishDate))
                {
                    sqlStr.Append(" AND StartDate<=@FinishDate ");
                    parameters.Add("FinishDate", finishDate);
                }
                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<Classes>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Classes> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_classes WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<Classes>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Classes> GetModel(Classes model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_classes WHERE Name=@Name AND TenantId=@TenantId ");
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }
                return await connection.QueryFirstOrDefaultAsync<Classes>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(Classes model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" INSERT INTO t_ea_classes (Id,`Name`,CourseId,CourseName,ClassSize,OwnedStudents,TeacherId,TeacherName,StartDate,RecruitStatus,ClassRoomId,ClassRoomName,TypeOfClass,TenantId,IsDeleted,CreateTime) ");
                sqlStr.Append(" VALUES ");
                sqlStr.Append(" (@Id,@Name,@CourseId,@CourseName,@ClassSize,@OwnedStudents,@TeacherId,@TeacherName,@StartDate,@RecruitStatus,@ClassRoomId,@ClassRoomName,@TypeOfClass,@TenantId,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(Classes model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE t_ea_classes SET `Name`=@Name,CourseId=@CourseId,CourseName=@CourseName,ClassSize=@ClassSize,TeacherId=@TeacherId,TeacherName=@TeacherName,StartDate=@StartDate,RecruitStatus=@RecruitStatus,ClassRoomId=@ClassRoomId,ClassRoomName=@ClassRoomName,TypeOfClass=@TypeOfClass WHERE Id=@Id ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新 -> 班级招生状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> UpdateRecruitStatus(Classes model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE t_ea_classes SET RecruitStatus=@RecruitStatus WHERE Id=@Id ");
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
                string sqlStr = "DELETE FROM t_ea_classes WHERE Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
