using Awine.Framework.Core.Collections;
using Awine.Framework.Dapper.Extensions.Options;
using Awine.Teach.FinancialService.Domain;
using Awine.Teach.FinancialService.Domain.Interface;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FinancialService.Infrastructure.Repository
{
    /// <summary>
    /// 日常支出
    /// </summary>
    public class DailySpendingRepository : IDailySpendingRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<DailySpendingRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public DailySpendingRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<DailySpendingRepository> logger)
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
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <param name="financialItemId"></param>
        /// <returns></returns>
        public async Task<IPagedList<DailySpending>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string beginDate = "", string finishDate = "", string financialItemId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_daily_spending WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(beginDate))
                {
                    sqlStr.Append(" AND CreateTime>=@BeginDate ");
                    parameters.Add("BeginDate", beginDate);
                }

                if (!string.IsNullOrEmpty(finishDate))
                {
                    sqlStr.Append(" AND CreateTime<=@FinishDate ");
                    parameters.Add("FinishDate", finishDate);
                }

                if (!string.IsNullOrEmpty(financialItemId))
                {
                    sqlStr.Append(" AND FinancialItemId=@FinancialItemId ");
                    parameters.Add("FinancialItemId", financialItemId);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<DailySpending>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="financialItemId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DailySpending>> GetAll(string tenantId = "", string financialItemId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_daily_spending WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(financialItemId))
                {
                    sqlStr.Append(" AND FinancialItemId=@FinancialItemId ");
                    parameters.Add("FinancialItemId", financialItemId);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                return await connection.QueryAsync<DailySpending>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(DailySpending model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("INSERT INTO t_daily_spending (Id,Name,`Amount`,`FinancialItemId`,FinancialItemName,TenantId,IsDeleted,CreateTime) VALUES (@Id,@Name,@Amount,@FinancialItemId,@FinancialItemName,@TenantId,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DailySpending model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE t_daily_spending SET Name=@Name,`Amount`=@Amount,FinancialItemId=@FinancialItemId,FinancialItemName=@FinancialItemName WHERE Id=@Id ");
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
                string sqlStr = "DELETE FROM t_daily_spending WHERE Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
