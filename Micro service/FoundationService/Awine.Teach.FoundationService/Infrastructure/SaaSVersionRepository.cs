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
    /// SaaS版本
    /// </summary>
    public class SaaSVersionRepository : ISaaSVersionRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<SaaSVersionRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public SaaSVersionRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<SaaSVersionRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        public async Task<IPagedList<SaaSVersion>> GetPageList(int page = 1, int limit = 15, string name = "", string identifying = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT Id,Name,Identifying,DisplayOrder,IsDeleted,CreateTime FROM SaaSVersions WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(name))
                {
                    sqlStr.Append(" AND Name = @Name ");
                    parameters.Add("Name", name);
                }

                if (!string.IsNullOrEmpty(identifying))
                {
                    sqlStr.Append(" AND Identifying = @Identifying ");
                    parameters.Add("Identifying", identifying);
                }

                sqlStr.Append(" ORDER BY DisplayOrder ");

                var list = await connection.QueryAsync<SaaSVersion>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SaaSVersion>> GetAll(string name = "", string identifying = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT Id,Name,Identifying,DisplayOrder,IsDeleted,CreateTime FROM SaaSVersions WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(name))
                {
                    sqlStr.Append(" AND Name = @Name ");
                    parameters.Add("Name", name);
                }

                if (!string.IsNullOrEmpty(identifying))
                {
                    sqlStr.Append(" AND Identifying = @Identifying ");
                    parameters.Add("Identifying", identifying);
                }

                sqlStr.Append(" ORDER BY DisplayOrder ");

                return await connection.QueryAsync<SaaSVersion>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tenantsDefaultSettingsModel"></param>
        /// <returns></returns>
        public async Task<bool> Add(SaaSVersion model, TenantDefaultSettings tenantsDefaultSettingsModel)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //写SaaS版本
                        sqlStr.Append(" INSERT INTO SaaSVersions ");
                        sqlStr.Append(" (Id,Name,Identifying,DisplayOrder,IsDeleted,CreateTime) ");
                        sqlStr.Append(" VALUES");
                        sqlStr.Append(" (@Id,@Name,@Identifying,@DisplayOrder,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写SaaS版本默认参数配置
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO TenantsDefaultSettings (Id,MaxNumberOfBranch,MaxNumberOfDepartments,MaxNumberOfRoles,MaxNumberOfUser,MaxNumberOfCourse,MaxNumberOfClass,MaxNumberOfClassRoom,MaxNumberOfStudent,MaxStorageSpace,SaaSVersionId,IsDeleted,CreateTime) VALUES (@Id,@MaxNumberOfBranch,@MaxNumberOfDepartments,@MaxNumberOfRoles,@MaxNumberOfUser,@MaxNumberOfCourse,@MaxNumberOfClass,@MaxNumberOfClassRoom,@MaxNumberOfStudent,@MaxStorageSpace,@SaaSVersionId,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), tenantsDefaultSettingsModel, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"添加SaaS版本时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(SaaSVersion model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" update SaaSVersions set ");
                sqlStr.Append(" Name=@Name,Identifying=@Identifying,DisplayOrder=@DisplayOrder,CreateTime=@CreateTime");
                sqlStr.Append(" where Id=@Id");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        sqlStr.Append(" DELETE FROM SaaSVersions where Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        sqlStr.Clear();
                        sqlStr.Append(" DELETE FROM TenantsDefaultSettings where SaaSVersionId=@SaaSVersionId ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { SaaSVersionId = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"删除SaaS版本时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SaaSVersion> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM SaaSVersions WHERE Id=@Id");
                return await connection.QueryFirstOrDefaultAsync<SaaSVersion>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SaaSVersion> GetModel(SaaSVersion model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" SELECT * FROM SaaSVersions WHERE Name=@Name AND Id!=@Id ");
                }
                else
                {
                    sqlStr.Append(" SELECT * FROM SaaSVersions WHERE Name=@Name ");
                }
                return await connection.QueryFirstOrDefaultAsync<SaaSVersion>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
