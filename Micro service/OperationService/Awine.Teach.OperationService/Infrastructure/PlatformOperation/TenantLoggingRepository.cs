using Awine.Framework.Core.Collections;
using Awine.Framework.Dapper.Extensions.Options;
using Awine.Teach.OperationService.Domain;
using Awine.Teach.OperationService.Domain.Interface;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.OperationService.Infrastructure.Repository
{
    /// <summary>
    /// 租户用户登录
    /// </summary>
    public class TenantLoggingRepository : ITenantLoggingRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<TenantLoggingRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public TenantLoggingRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<TenantLoggingRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="account"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IPagedList<TenantLogging>> GetPageList(int page = 1, int limit = 15, string account = "", string tenantId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM t_tenant_logon_log WHERE IsDeleted=0  ");

                if (!string.IsNullOrEmpty(account))
                {
                    sqlStr.Append("  AND Account=@Account ");
                    parameters.Add("Account", account);
                }

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append("  AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<TenantLogging>(sqlStr.ToString(), commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(TenantLogging model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("INSERT INTO t_tenant_logon_log (Id,`Account`,`UserName`,`TenantId`,TenantName,LogonIPAddress,IsDeleted,CreateTime) VALUES (@Id,@Account,@UserName,@TenantId,@TenantName,@LogonIPAddress,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut,
                    commandType: CommandType.Text);
            }
        }
    }
}
