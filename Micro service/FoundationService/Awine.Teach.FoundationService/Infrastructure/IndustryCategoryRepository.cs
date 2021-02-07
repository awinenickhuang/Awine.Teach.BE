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
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Infrastructure.Repository
{
    /// <summary>
    /// 行业
    /// </summary>
    public class IndustryCategoryRepository : IIndustryCategoryRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<IndustryCategoryRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public IndustryCategoryRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<IndustryCategoryRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IPagedList<IndustryCategory>> GetPageList(int pageIndex = 1, int pageSize = 15)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM industrycategory ORDER BY DisplayOrder ");

                var list = await connection.QueryAsync<IndustryCategory>(sqlStr.ToString(), commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(pageIndex, pageSize);
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IndustryCategory>> GetAll()
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM industrycategory ORDER BY DisplayOrder ");

                return await connection.QueryAsync<IndustryCategory>(sqlStr.ToString(), commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IndustryCategory> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();
                sqlStr.Append(" SELECT * FROM industrycategory WHERE Id=@Id ");
                parameters.Add("Id", id);
                return await connection.QueryFirstOrDefaultAsync<IndustryCategory>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut,
                    commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IndustryCategory> GetModel(IndustryCategory model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM industrycategory WHERE Name=@Name ");
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }
                return await connection.QueryFirstOrDefaultAsync<IndustryCategory>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut,
                   commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(IndustryCategory model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("INSERT INTO industrycategory (Id,`Name`,`DescText`,DisplayOrder,CreateTime) VALUES (@Id,@Name,@DescText,@DisplayOrder,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut,
                    commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(IndustryCategory model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE industrycategory SET `Name`=@Name,`DescText`=@DescText,DisplayOrder=@DisplayOrder WHERE Id=@Id ");
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
                string sqlStr = "DELETE FROM industrycategory WHERE Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
