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
    /// 租户信息设置
    /// </summary>
    public class TenantSettingsRepository : ITenantSettingsRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<TenantSettingsRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public TenantSettingsRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<TenantSettingsRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IPagedList<TenantSettings>> GetPageList(int page = 1, int limit = 15, string tenantId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM TenantSettings WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<TenantSettings>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TenantSettings>> GetAll(string tenantId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM TenantSettings WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                return await connection.QueryAsync<TenantSettings>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<TenantSettings> GetTenantSettings(string tenantId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM TenantSettings WHERE TenantId=@TenantId ");
                return await connection.QueryFirstOrDefaultAsync<TenantSettings>(sqlStr.ToString(), new { TenantId = tenantId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TenantSettings> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM TenantSettings WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<TenantSettings>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<TenantSettings> GetModel(TenantSettings model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM TenantSettings WHERE TenantId=@TenantId ");
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }
                return await connection.QueryFirstOrDefaultAsync<TenantSettings>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(TenantSettings model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("INSERT INTO TenantSettings (Id,MaxNumberOfBranch,MaxNumberOfDepartments,MaxNumberOfRoles,`MaxNumberOfUser`,`MaxNumberOfCourse`,MaxNumberOfClass,MaxNumberOfClassRoom,MaxNumberOfStudent,MaxStorageSpace,AvailableStorageSpace,UsedStorageSpace,TenantId,IsDeleted,CreateTime) VALUES (@Id,@MaxNumberOfBranch,@MaxNumberOfDepartments,@MaxNumberOfRoles,@MaxNumberOfUser,@MaxNumberOfCourse,@MaxNumberOfClass,@MaxNumberOfClassRoom,@MaxNumberOfStudent,@MaxStorageSpace,@AvailableStorageSpace,@UsedStorageSpace,@TenantId,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(TenantSettings model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE TenantSettings SET MaxNumberOfBranch=@MaxNumberOfBranch,MaxNumberOfDepartments=@MaxNumberOfDepartments,MaxNumberOfRoles=@MaxNumberOfRoles,`MaxNumberOfUser`=@MaxNumberOfUser,MaxNumberOfCourse=@MaxNumberOfCourse,MaxNumberOfClass=@MaxNumberOfClass,MaxNumberOfClassRoom=@MaxNumberOfClassRoom,MaxNumberOfStudent=@MaxNumberOfStudent,MaxStorageSpace=@MaxStorageSpace,AvailableStorageSpace=@AvailableStorageSpace,UsedStorageSpace=@UsedStorageSpace WHERE Id=@Id ");
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
                string sqlStr = "DELETE FROM TenantSettings WHERE Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
