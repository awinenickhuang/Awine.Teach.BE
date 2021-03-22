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
    /// 机构信息设置 不同的应用版本对应不同的配置
    /// </summary>
    public class TenantDefaultSettingsRepository : ITenantDefaultSettingsRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<TenantDefaultSettingsRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public TenantDefaultSettingsRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<TenantDefaultSettingsRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<TenantDefaultSettings>> GetPageList(int page = 1, int limit = 15)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM TenantsDefaultSettings WHERE IsDeleted=0 ");

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<TenantDefaultSettings>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="appVersionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TenantDefaultSettings>> GetAll(string appVersionId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM TenantsDefaultSettings WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(appVersionId))
                {
                    sqlStr.Append(" AND AppVersionId=@AppVersionId ");
                    parameters.Add("AppVersionId", appVersionId);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                return await connection.QueryAsync<TenantDefaultSettings>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TenantDefaultSettings> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM TenantsDefaultSettings WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<TenantDefaultSettings>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TenantDefaultSettings> GetModel(TenantDefaultSettings model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM TenantsDefaultSettings WHERE AppVersionId=@AppVersionId ");
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }
                return await connection.QueryFirstOrDefaultAsync<TenantDefaultSettings>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(TenantDefaultSettings model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("INSERT INTO TenantsDefaultSettings (Id,NumberOfBranches,`MaxNumberOfUser`,`MaxNumberOfCourse`,MaxNumberOfClass,MaxNumberOfStudent,MaxStorageSpace,AppVersionId,IsDeleted,CreateTime) VALUES (@Id,@NumberOfBranches,@MaxNumberOfUser,@MaxNumberOfCourse,@MaxNumberOfClass,@MaxNumberOfStudent,@MaxStorageSpace,@AppVersionId,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(TenantDefaultSettings model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE TenantsDefaultSettings SET NumberOfBranches=@NumberOfBranches,`MaxNumberOfUser`=@MaxNumberOfUser,MaxNumberOfCourse=@MaxNumberOfCourse,MaxNumberOfClass=@MaxNumberOfClass,MaxNumberOfStudent=@MaxNumberOfStudent,MaxStorageSpace=@MaxStorageSpace,AppVersionId=@AppVersionId WHERE Id=@Id ");
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
                string sqlStr = "DELETE FROM TenantsDefaultSettings WHERE Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
