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
    /// 按钮
    /// </summary>
    public class ButtonsRepository : IButtonsRepository
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
        public ButtonsRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<ButtonsRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Buttons>> GetAll()
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM Buttons ORDER BY DisplayOrder ");
                return await connection.QueryAsync<Buttons>(sqlStr.ToString(), commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 某一模块拥有的按钮列表
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Buttons>> GetModuleButtons(string moduleId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM Buttons WHERE ModuleId=@ModuleId ORDER BY DisplayOrder ");
                return await connection.QueryAsync<Buttons>(sqlStr.ToString(), new { ModuleId = moduleId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 某一模块拥有的按钮 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<IPagedList<Buttons>> GetPageList(int page = 1, int limit = 15, string moduleId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM Buttons WHERE ModuleId=@ModuleId ORDER BY DisplayOrder ");

                var list = await connection.QueryAsync<Buttons>(sqlStr.ToString(), new { ModuleId = moduleId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(Buttons model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" insert into Buttons ");
                sqlStr.Append(" (Id,ModuleId,Name,ButtonIcon,AccessCode,DisplayOrder,CreateTime) ");
                sqlStr.Append(" values");
                sqlStr.Append(" (@Id,@ModuleId,@Name,@ButtonIcon,@AccessCode,@DisplayOrder,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(Buttons model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" update Buttons set ");
                sqlStr.Append(" Name=@Name,ButtonIcon=@ButtonIcon,AccessCode=@AccessCode,DisplayOrder=@DisplayOrder,CreateTime=@CreateTime");
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
                string sqlStr = "delete from Buttons where Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Buttons> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM Buttons WHERE Id=@Id");
                return await connection.QueryFirstOrDefaultAsync<Buttons>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Buttons> GetModel(Buttons model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" SELECT * FROM Buttons WHERE Id!=@Id AND AccessCode=@AccessCode ");
                }
                else
                {
                    sqlStr.Append(" SELECT * FROM Buttons WHERE AccessCode=@AccessCode ");
                }
                return await connection.QueryFirstOrDefaultAsync<Buttons>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
