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
    /// 系统模块
    /// </summary>
    public class ModulesRepository : IModulesRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<ModulesRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public ModulesRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<ModulesRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Modules>> GetAll()
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("SELECT * FROM modules order by DisplayOrder ");
                return await connection.QueryAsync<Modules>(sqlStr.ToString(), commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<Modules>> GetPageList(int page = 1, int limit = 15)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM modules ORDER BY DisplayOrder");

                var list = await connection.QueryAsync<Modules>(sqlStr.ToString(), commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 获取一个模块的子模块列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Modules>> GetAllChilds(string parentId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("SELECT * FROM modules where ParentId=@ParentId order by DisplayOrder ");
                return await connection.QueryAsync<Modules>(sqlStr.ToString(), new { ParentId = parentId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(Modules model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" INSERT INTO modules (Id,Name,ParentId,DescText,IsEnable,RedirectUri,ModuleIcon,DisplayOrder,CreateTime) VALUES (@Id,@Name,@ParentId,@DescText,@IsEnable,@RedirectUri,@ModuleIcon,@DisplayOrder,@CreateTime)");
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
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" DELETE FROM modules WHERE Id=@Id");
                return await connection.ExecuteAsync(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(Modules model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE modules SET ");
                sqlStr.Append(" Name=@Name,ParentId=@ParentId,DescText=@DescText,IsEnable=@IsEnable,RedirectUri=@RedirectUri,ModuleIcon=@ModuleIcon,DisplayOrder=@DisplayOrder");
                sqlStr.Append(" WHERE Id=@Id");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Modules> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                string sqlStr = " SELECT * FROM modules WHERE Id=@Id";
                return await connection.QueryFirstOrDefaultAsync<Modules>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Modules> GetModel(Modules model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" SELECT * FROM modules WHERE Id !=@Id and Name=@Name ");
                }
                else
                {
                    sqlStr.Append(" SELECT * FROM modules WHERE Name=@Name ");
                }
                return await connection.QueryFirstOrDefaultAsync<Modules>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
