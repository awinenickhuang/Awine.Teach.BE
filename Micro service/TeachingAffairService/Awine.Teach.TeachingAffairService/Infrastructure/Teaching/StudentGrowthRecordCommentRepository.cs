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
    /// 学生成长记录 -> 评论
    /// </summary>
    public class StudentGrowthRecordCommentRepository : IStudentGrowthRecordCommentRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<StudentGrowthRecordCommentRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public StudentGrowthRecordCommentRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<StudentGrowthRecordCommentRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="studentGrowthRecordId"></param>
        /// <returns></returns>
        public async Task<IPagedList<StudentGrowthRecordComment>> GetPageList(int page = 1, int limit = 15, string studentGrowthRecordId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_ea_student_growthrecord_comment WHERE IsDeleted=0 ");

                //TODO studentGrowthRecordId 参数是必须的
                //if (!string.IsNullOrEmpty(studentGrowthRecordId))
                //{
                sqlStr.Append(" AND StudentGrowthRecordId=@StudentGrowthRecordId ");
                parameters.Add("StudentGrowthRecordId", studentGrowthRecordId);
                //}

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<StudentGrowthRecordComment>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StudentGrowthRecordComment> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_student_growthrecord_comment WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<StudentGrowthRecordComment>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(StudentGrowthRecordComment model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("INSERT INTO t_ea_student_growthrecord_comment (Id,`StudentGrowthRecordId`,Contents,`CreatorId`,`CreatorName`,TenantId,IsDeleted,CreateTime) VALUES (@Id,@StudentGrowthRecordId,@Contents@CreatorId,@CreatorName,@TenantId,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
