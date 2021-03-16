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
    /// 角色权限 -> 角色拥有的模块信息
    /// </summary>
    public class RolesOwnedModulesRepository : IRolesOwnedModulesRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<RolesOwnedModulesRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public RolesOwnedModulesRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<RolesOwnedModulesRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 保存角色操作权限 -> 模块及按钮信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="aspnetrolesOwnedModules"></param>
        /// <param name="aspnetroleClaims"></param>
        /// <returns></returns>
        public async Task<bool> SaveRoleOwnedModules(string roleId, IList<RolesOwnedModules> aspnetrolesOwnedModules, IList<RolesClaims> aspnetroleClaims)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //删除
                        sqlStr.Clear();
                        sqlStr.Append(" DELETE FROM RolesOwnedModules WHERE RoleId=@RoleId ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { RoleId = roleId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        sqlStr.Clear();
                        sqlStr.Append(" DELETE FROM RolesClaims WHERE RoleId=@RoleId ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { RoleId = roleId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写入
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO RolesOwnedModules ");
                        sqlStr.Append(" (Id,RoleId,ModuleId,TenantId) VALUES (@Id,@RoleId,@ModuleId,@TenantId)");
                        await connection.ExecuteAsync(sqlStr.ToString(), aspnetrolesOwnedModules, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO RolesClaims ");
                        sqlStr.Append(" (Id,RoleId,ClaimType,ClaimValue) VALUES (@Id,@RoleId,@ClaimType,@ClaimValue)");
                        await connection.ExecuteAsync(sqlStr.ToString(), aspnetroleClaims, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"设置角色拥有的模块信息时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 查询 -> 角色拥有的模块信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RolesOwnedModules>> GetRoleOwnedModules(string roleId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("SELECT * FROM RolesOwnedModules WHERE RoleId=@RoleId");
                return await connection.QueryAsync<RolesOwnedModules>(sqlStr.ToString(), new { RoleId = roleId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 获取模块集合，以识别模块是否被角色权限使用
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RolesOwnedModules>> GetModels(string moduleId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                string sqlStr = " SELECT * FROM RolesOwnedModules WHERE ModuleId=@ModuleId";
                return await connection.QueryAsync<RolesOwnedModules>(sqlStr.ToString(), new { ModuleId = moduleId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
