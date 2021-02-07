using Awine.Framework.Core.Collections;
using Awine.Framework.Dapper.Extensions.Options;
using Awine.WebSite.Domain;
using Awine.WebSite.Domain.Interface;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Awine.WebSite.Infrastructure.Repository
{
    /// <summary>
    /// 版块管理
    /// </summary>
    public class ForumRepository : IForumRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<ForumRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public ForumRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<ForumRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="displayAttribute"></param>
        /// <param name="contentAttribute"></param>
        /// <returns></returns>
        public async Task<IPagedList<Forum>> GetPageList(int page = 1, int limit = 15, int displayAttribute = 0, int contentAttribute = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM t_forum WHERE 1=1 ");

                if (displayAttribute > 0)
                {
                    sqlStr.Append(" AND DisplayAttribute=@DisplayAttribute ");
                    parameters.Add("DisplayAttribute", displayAttribute);
                }

                if (contentAttribute > 0)
                {
                    sqlStr.Append(" AND ContentAttribute=@ContentAttribute ");
                    parameters.Add("ContentAttribute", contentAttribute);
                }

                sqlStr.Append(" ORDER BY DisplayOrder ");

                var list = await connection.QueryAsync<Forum>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="displayAttribute"></param>
        /// <param name="contentAttribute"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Forum>> GetAll(int displayAttribute = 0, int contentAttribute = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM t_forum WHERE 1=1 ");

                if (displayAttribute > 0)
                {
                    sqlStr.Append(" AND DisplayAttribute=@DisplayAttribute ");
                    parameters.Add("DisplayAttribute", displayAttribute);
                }

                if (contentAttribute > 0)
                {
                    sqlStr.Append(" AND ContentAttribute=@ContentAttribute ");
                    parameters.Add("ContentAttribute", contentAttribute);
                }

                sqlStr.Append(" ORDER BY DisplayOrder ");
                return await connection.QueryAsync<Forum>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Forum>> GetAllChilds(string parentId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_forum WHERE ParentId=@ParentId ORDER BY DisplayOrder ");
                return await connection.QueryAsync<Forum>(sqlStr.ToString(), new { ParentId = parentId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Forum> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_forum WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<Forum>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut,
                    commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(Forum model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("INSERT INTO t_forum (Id,`Name`,EnglishName,`DescribeText`,`ParentId`,DisplayAttribute,DisplayOrder,ContentAttribute,RedirectAddress,CreateTime) VALUES (@Id,@Name,@EnglishName,@DescribeText,@ParentId,@DisplayAttribute,@DisplayOrder,@ContentAttribute,@RedirectAddress,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut,
                    commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(Forum model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE t_forum SET Name=@Name,EnglishName=@EnglishName,`DescribeText`=@DescribeText,ParentId=@ParentId, ");
                sqlStr.Append(" DisplayAttribute=@DisplayAttribute,DisplayOrder=@DisplayOrder,");
                sqlStr.Append(" ContentAttribute=@ContentAttribute,RedirectAddress=@RedirectAddress ");
                sqlStr.Append(" WHERE Id=@Id ");
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
                string sqlStr = "DELETE FROM t_forum WHERE Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
