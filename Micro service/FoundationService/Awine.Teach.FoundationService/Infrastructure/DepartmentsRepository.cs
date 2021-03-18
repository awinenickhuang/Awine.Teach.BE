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
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Infrastructure.Repository
{
    /// <summary>
    /// 部门
    /// </summary>
    public class DepartmentsRepository : IDepartmentsRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<DepartmentsRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public DepartmentsRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<DepartmentsRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IPagedList<Departments>> GetPageList(int pageIndex = 1, int pageSize = 15, string tenantId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();
                sqlStr.Append(" SELECT departments.*,tenant.Id,tenant.Name");
                sqlStr.Append(" FROM Departments as departments");
                sqlStr.Append(" LEFT JOIN Tenants as tenant on departments.TenantId=tenant.Id WHERE departments.IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND departments.TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }
                sqlStr.Append(" ORDER BY DisplayOrder ");

                var result = await connection.QueryAsync<Departments, Tenants, Departments>(sqlStr.ToString(), (departments, tenant)
                     =>
                {
                    departments.Tenant = tenant;
                    return departments;
                }, parameters, splitOn: "Id", commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
                
                return result.ToPagedList(pageIndex, pageSize);
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Departments>> GetAll(string tenantId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM Departments WHERE IsDeleted=0 ");
                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }
                sqlStr.Append(" ORDER BY TenantId, DisplayOrder ");

                return await connection.QueryAsync<Departments>(sqlStr.ToString(), new { TenantId = tenantId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Departments> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();
                sqlStr.Append(" SELECT * FROM Departments WHERE Id=@Id ");
                parameters.Add("Id", id);
                return await connection.QueryFirstOrDefaultAsync<Departments>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut,                    commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Departments> GetModel(Departments model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM Departments WHERE Name=@Name AND TenantId=@TenantId ");
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }
                return await connection.QueryFirstOrDefaultAsync<Departments>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(Departments model)
        {
            model.IsDeleted = false;
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("INSERT INTO Departments (Id,TenantId,`Name`,`Describe`,DisplayOrder,IsDeleted,CreateTime) VALUES (@Id,@TenantId,@Name,@Describe,@DisplayOrder,@IsDeleted,@CreateTime) ");
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
                string sqlStr = "UPDATE Departments SET IsDeleted=@IsDeleted WHERE Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { IsDeleted = true, Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(Departments model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE Departments SET TenantId=@TenantId,`Name`=@Name,`Describe`=@Describe,DisplayOrder=@DisplayOrder WHERE Id=@Id ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
