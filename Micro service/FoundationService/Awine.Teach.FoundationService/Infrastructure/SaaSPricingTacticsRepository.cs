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
    /// SaaS版本定价策略
    /// </summary>
    public class SaaSPricingTacticsRepository : ISaaSPricingTacticsRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<SaaSPricingTacticsRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public SaaSPricingTacticsRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<SaaSPricingTacticsRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SaaSPricingTactics>> GetAll(string saaSVersionId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM SaaSPricingTactics WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(saaSVersionId))
                {
                    sqlStr.Append(" AND SaaSVersionId = @SaaSVersionId ");
                    parameters.Add("SaaSVersionId", saaSVersionId);
                }

                return await connection.QueryAsync<SaaSPricingTactics>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        public async Task<IPagedList<SaaSPricingTactics>> GetPageList(int page = 1, int limit = 15, string saaSVersionId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM SaaSPricingTactics WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(saaSVersionId))
                {
                    sqlStr.Append(" AND SaaSVersionId = @SaaSVersionId ");
                    parameters.Add("SaaSVersionId", saaSVersionId);
                }

                var list = await connection.QueryAsync<SaaSPricingTactics>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SaaSPricingTactics> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM SaaSPricingTactics WHERE Id=@Id");
                return await connection.QueryFirstOrDefaultAsync<SaaSPricingTactics>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SaaSPricingTactics> GetModel(SaaSPricingTactics model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" SELECT Id FROM SaaSPricingTactics WHERE Id!=@Id AND (SaaSVersionId=@SaaSVersionId AND NumberOfYears=@NumberOfYears AND ChargeRates=@ChargeRates) ");
                }
                else
                {
                    sqlStr.Append(" SELECT Id FROM SaaSPricingTactics WHERE SaaSVersionId=@SaaSVersionId AND NumberOfYears=@NumberOfYears AND ChargeRates=@ChargeRates ");
                }
                return await connection.QueryFirstOrDefaultAsync<SaaSPricingTactics>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(SaaSPricingTactics model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" INSERT INTO SaaSPricingTactics ");
                sqlStr.Append(" (Id,SaaSVersionId,NumberOfYears,ChargeRates,IsDeleted,CreateTime) ");
                sqlStr.Append(" VALUES ");
                sqlStr.Append(" (@Id,@SaaSVersionId,@NumberOfYears,@ChargeRates,@IsDeleted,@CreateTime) ");

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
                string sqlStr = " DELETE FROM SaaSPricingTactics WHERE Id=@Id ";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(SaaSPricingTactics model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE SaaSPricingTactics SET ");
                sqlStr.Append(" SaaSVersionId=@SaaSVersionId,NumberOfYears=@NumberOfYears,ChargeRates=@ChargeRates");
                sqlStr.Append(" WHERE Id=@Id");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
