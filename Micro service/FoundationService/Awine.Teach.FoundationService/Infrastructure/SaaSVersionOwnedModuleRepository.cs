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
    /// SaaS版本包括的系统模块
    /// </summary>
    public class SaaSVersionOwnedModuleRepository : ISaaSVersionOwnedModuleRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<SaaSVersionOwnedModuleRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public SaaSVersionOwnedModuleRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<SaaSVersionOwnedModuleRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 设置SaaS版本包括的模块信息
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public async Task<bool> SaveSaaSVersionOwnedModules(string saaSVersionId, IList<SaaSVersionOwnedModule> modules)
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
                        sqlStr.Append(" DELETE FROM SaaSVersionsOwnedModules WHERE SaaSVersionId=@SaaSVersionId ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { SaaSVersionId = saaSVersionId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写入
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO SaaSVersionsOwnedModules ");
                        sqlStr.Append(" (Id,SaaSVersionId,ModuleId,IsDeleted,CreateTime) VALUES (@Id,@SaaSVersionId,@ModuleId,@IsDeleted,@CreateTime)");
                        await connection.ExecuteAsync(sqlStr.ToString(), modules, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"设置SaaS版本模块时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 查询SaaS版本包括的模块信息
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SaaSVersionOwnedModule>> GetSaaSVersionOwnedModules(string saaSVersionId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("SELECT * FROM SaaSVersionsOwnedModules WHERE SaaSVersionId=@SaaSVersionId");
                return await connection.QueryAsync<SaaSVersionOwnedModule>(sqlStr.ToString(), new { SaaSVersionId = saaSVersionId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 获取模块集合，以识别模块是否被SaaS版本使用
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SaaSVersionOwnedModule>> GetModels(string moduleId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                string sqlStr = " SELECT * FROM SaaSVersionsOwnedModules WHERE ModuleId=@ModuleId";
                return await connection.QueryAsync<SaaSVersionOwnedModule>(sqlStr.ToString(), new { ModuleId = moduleId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
