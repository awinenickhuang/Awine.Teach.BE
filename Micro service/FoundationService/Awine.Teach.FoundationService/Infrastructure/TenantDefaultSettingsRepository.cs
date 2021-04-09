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
    /// 机构信息设置 不同的SaaS版本对应不同的配置
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
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TenantDefaultSettings>> GetAll(string saaSVersionId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM TenantsDefaultSettings WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(saaSVersionId))
                {
                    sqlStr.Append(" AND SaaSVersionId=@SaaSVersionId ");
                    parameters.Add("SaaSVersionId", saaSVersionId);
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
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        public async Task<TenantDefaultSettings> GetModelForAppVersion(string saaSVersionId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM TenantsDefaultSettings WHERE SaaSVersionId=@SaaSVersionId ");
                return await connection.QueryFirstOrDefaultAsync<TenantDefaultSettings>(sqlStr.ToString(), new { SaaSVersionId = saaSVersionId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
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
                sqlStr.Append(" SELECT * FROM TenantsDefaultSettings WHERE SaaSVersionId=@SaaSVersionId ");
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
                sqlStr.Append("INSERT INTO TenantsDefaultSettings (Id,MaxNumberOfBranch,MaxNumberOfDepartments,MaxNumberOfRoles,`MaxNumberOfUser`,`MaxNumberOfCourse`,MaxNumberOfClass,MaxNumberOfClassRoom,MaxNumberOfStudent,MaxStorageSpace,SaaSVersionId,IsDeleted,CreateTime) VALUES (@Id,@MaxNumberOfBranch,@MaxNumberOfDepartments,@MaxNumberOfRoles,@MaxNumberOfUser,@MaxNumberOfCourse,@MaxNumberOfClass,@MaxNumberOfClassRoom,@MaxNumberOfStudent,@MaxStorageSpace,@SaaSVersionId,@IsDeleted,@CreateTime) ");
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
                sqlStr.Append(" UPDATE TenantsDefaultSettings SET MaxNumberOfBranch=@MaxNumberOfBranch,MaxNumberOfDepartments=@MaxNumberOfDepartments,MaxNumberOfRoles=@MaxNumberOfRoles,`MaxNumberOfUser`=@MaxNumberOfUser,MaxNumberOfCourse=@MaxNumberOfCourse,MaxNumberOfClass=@MaxNumberOfClass,MaxNumberOfClassRoom=@MaxNumberOfClassRoom,MaxNumberOfStudent=@MaxNumberOfStudent,MaxStorageSpace=@MaxStorageSpace,SaaSVersionId=@SaaSVersionId WHERE Id=@Id ");
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
