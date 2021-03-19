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
    /// 应用版本对应的系统模块
    /// </summary>
    public class ApplicationVersionOwnedModuleRepository : IApplicationVersionOwnedModuleRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<ApplicationVersionOwnedModuleRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public ApplicationVersionOwnedModuleRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<ApplicationVersionOwnedModuleRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 设置应用版本包括的模块信息
        /// </summary>
        /// <param name="appVersionId"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public async Task<bool> SaveAppVersionOwnedModules(string appVersionId, IList<ApplicationVersionOwnedModule> modules)
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
                        sqlStr.Append(" DELETE FROM ApplicationVersionsOwnedModules WHERE AppVersionId=@AppVersionId ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { AppVersionId = appVersionId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写入
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO ApplicationVersionsOwnedModules ");
                        sqlStr.Append(" (Id,AppVersionId,ModuleId,IsDeleted,CreateTime) VALUES (@Id,@AppVersionId,@ModuleId,@IsDeleted,@CreateTime)");
                        await connection.ExecuteAsync(sqlStr.ToString(), modules, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"设置应用版本模块时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 查询应用版本包括的模块信息
        /// </summary>
        /// <param name="appVersionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ApplicationVersionOwnedModule>> GetAppVersionOwnedModules(string appVersionId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("SELECT * FROM ApplicationVersionsOwnedModules WHERE AppVersionId=@AppVersionId");
                return await connection.QueryAsync<ApplicationVersionOwnedModule>(sqlStr.ToString(), new { AppVersionId = appVersionId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 获取模块集合，以识别模块是否被应用版本使用
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ApplicationVersionOwnedModule>> GetModels(string moduleId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                string sqlStr = " SELECT * FROM ApplicationVersionsOwnedModules WHERE ModuleId=@ModuleId";
                return await connection.QueryAsync<ApplicationVersionOwnedModule>(sqlStr.ToString(), new { ModuleId = moduleId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
