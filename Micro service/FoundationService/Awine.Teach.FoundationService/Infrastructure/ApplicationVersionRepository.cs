using Awine.Framework.Core.Collections;
using Awine.Framework.Dapper.Extensions.Options;
using Awine.Teach.FoundationService.Domain.Interface;
using Awine.Teach.FoundationService.Domain.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Infrastructure.Repository
{
    /// <summary>
    /// 应用版本
    /// </summary>
    public class ApplicationVersionRepository : IApplicationVersionRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<ButtonsRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public ApplicationVersionRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<ButtonsRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        public async Task<IPagedList<ApplicationVersion>> GetPageList(int page = 1, int limit = 15, string name = "", string identifying = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT Id,Name,Identifying,DisplayOrder,IsDeleted,CreateTime FROM ApplicationVersions WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(name))
                {
                    sqlStr.Append(" AND Name = @Name ");
                    parameters.Add("Name", name);
                }

                if (!string.IsNullOrEmpty(identifying))
                {
                    sqlStr.Append(" AND Identifying = @Identifying ");
                    parameters.Add("Identifying", identifying);
                }

                var list = await connection.QueryAsync<ApplicationVersion>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ApplicationVersion>> GetAll(string name = "", string identifying = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT Id,Name,Identifying,DisplayOrder,IsDeleted,CreateTime FROM ApplicationVersions WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(name))
                {
                    sqlStr.Append(" AND Name = @Name ");
                    parameters.Add("Name", name);
                }

                if (!string.IsNullOrEmpty(identifying))
                {
                    sqlStr.Append(" AND Identifying = @Identifying ");
                    parameters.Add("Identifying", identifying);
                }

                return await connection.QueryAsync<ApplicationVersion>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(ApplicationVersion model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" insert into ApplicationVersions ");
                sqlStr.Append(" (Id,Name,Identifying,DisplayOrder,IsDeleted,CreateTime) ");
                sqlStr.Append(" values");
                sqlStr.Append(" (@Id,@Name,@Identifying,@DisplayOrder,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(ApplicationVersion model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" update ApplicationVersions set ");
                sqlStr.Append(" Name=@Name,Identifying=@Identifying,DisplayOrder=@DisplayOrder,CreateTime=@CreateTime");
                sqlStr.Append(" where Id=@Id");
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
                string sqlStr = "delete from ApplicationVersions where Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationVersion> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM ApplicationVersions WHERE Id=@Id");
                return await connection.QueryFirstOrDefaultAsync<ApplicationVersion>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApplicationVersion> GetModel(ApplicationVersion model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" SELECT * FROM ApplicationVersions WHERE Id!=@Id AND (Name=@Name OR Identifying=@Identifying) ");
                }
                else
                {
                    sqlStr.Append(" SELECT * FROM ApplicationVersions WHERE Name=@Name OR Identifying=@Identifying ");
                }
                return await connection.QueryFirstOrDefaultAsync<ApplicationVersion>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
