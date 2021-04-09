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
    /// 订单信息
    /// </summary>
    public class OrdersRepository : IOrdersRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<OrdersRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public OrdersRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<OrdersRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="tradeCategories"></param>
        /// <param name="performanceOwnerId"></param>
        /// <param name="performanceTenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Orders>> GetAll(string tenantId = "", string saaSVersionId = "", int tradeCategories = 0, string performanceOwnerId = "", string performanceTenantId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM Orders WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId = @TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(saaSVersionId))
                {
                    sqlStr.Append(" AND SaaSVersionId = @SaaSVersionId ");
                    parameters.Add("SaaSVersionId", saaSVersionId);
                }

                if (tradeCategories > 0)
                {
                    sqlStr.Append(" AND TradeCategories = @TradeCategories ");
                    parameters.Add("TradeCategories", tradeCategories);
                }

                if (!string.IsNullOrEmpty(performanceOwnerId))
                {
                    sqlStr.Append(" AND PerformanceOwnerId = @PerformanceOwnerId ");
                    parameters.Add("PerformanceOwnerId", performanceOwnerId);
                }

                if (!string.IsNullOrEmpty(performanceTenantId))
                {
                    sqlStr.Append(" AND PerformanceTenantId = @PerformanceTenantId ");
                    parameters.Add("PerformanceTenantId", performanceTenantId);
                }

                return await connection.QueryAsync<Orders>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="tradeCategories"></param>
        /// <param name="performanceOwnerId"></param>
        /// <param name="performanceTenantId"></param>
        /// <returns></returns>
        public async Task<IPagedList<Orders>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string saaSVersionId = "", int tradeCategories = 0, string performanceOwnerId = "", string performanceTenantId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM Orders WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId = @TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(saaSVersionId))
                {
                    sqlStr.Append(" AND SaaSVersionId = @SaaSVersionId ");
                    parameters.Add("SaaSVersionId", saaSVersionId);
                }

                if (tradeCategories > 0)
                {
                    sqlStr.Append(" AND TradeCategories = @TradeCategories ");
                    parameters.Add("TradeCategories", tradeCategories);
                }

                if (!string.IsNullOrEmpty(performanceOwnerId))
                {
                    sqlStr.Append(" AND PerformanceOwnerId = @PerformanceOwnerId ");
                    parameters.Add("PerformanceOwnerId", performanceOwnerId);
                }

                if (!string.IsNullOrEmpty(performanceTenantId))
                {
                    sqlStr.Append(" AND PerformanceTenantId = @PerformanceTenantId ");
                    parameters.Add("PerformanceTenantId", performanceTenantId);
                }

                var list = await connection.QueryAsync<Orders>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Add(Orders model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //写版本
                        sqlStr.Append(" INSERT INTO Orders ");
                        sqlStr.Append(" (Id,TenantId,TenantName,NumberOfYears,PayTheAmount,PerformanceOwnerId,PerformanceOwner,PerformanceTenantId,PerformanceTenant,TradeCategories,SaaSVersionId,AppVersionName,IsDeleted,CreateTime) ");
                        sqlStr.Append(" VALUES");
                        sqlStr.Append(" (@Id,@TenantId,@TenantName,@NumberOfYears,@PayTheAmount,@PerformanceOwnerId,@PerformanceOwner,@PerformanceTenantId,@PerformanceTenant,@TradeCategories,@SaaSVersionId,@AppVersionName,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"添加系统版本时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Orders> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM Orders WHERE Id=@Id");
                return await connection.QueryFirstOrDefaultAsync<Orders>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
