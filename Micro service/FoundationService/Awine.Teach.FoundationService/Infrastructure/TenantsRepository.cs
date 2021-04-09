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
    /// 租户信息
    /// </summary>
    public class TenantsRepository : ITenantsRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<TenantsRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public TenantsRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<TenantsRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 全部数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="classiFication"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="status"></param>
        /// <param name="industryId"></param>
        /// <param name="creatorId"></param>
        /// <param name="creatorTenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Tenants>> GetAll(string tenantId = "", int classiFication = 0, string saaSVersionId = "", int status = 0, string industryId = "", string creatorId = "", string creatorTenantId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();

                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM `Tenants` WHERE `IsDeleted`=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND Id=@Id ");
                    parameters.Add("Id", tenantId);
                }

                if (classiFication > 0)
                {
                    sqlStr.Append(" AND ClassiFication=@ClassiFication ");
                    parameters.Add("ClassiFication", classiFication);
                }

                if (!string.IsNullOrEmpty(saaSVersionId))
                {
                    sqlStr.Append(" AND SaaSVersionId=@SaaSVersionId ");
                    parameters.Add("SaaSVersionId", saaSVersionId);
                }

                if (status > 0)
                {
                    sqlStr.Append(" AND Status=@Status ");
                    parameters.Add("Status", status);
                }

                if (!string.IsNullOrEmpty(industryId))
                {
                    sqlStr.Append(" AND IndustryId=@IndustryId ");
                    parameters.Add("IndustryId", industryId);
                }

                if (!string.IsNullOrEmpty(creatorId))
                {
                    sqlStr.Append(" AND CreatorId=@CreatorId ");
                    parameters.Add("CreatorId", creatorId);
                }

                if (!string.IsNullOrEmpty(creatorTenantId))
                {
                    sqlStr.Append(" AND CreatorTenantId=@CreatorTenantId ");
                    parameters.Add("CreatorTenantId", creatorTenantId);
                }

                return await connection.QueryAsync<Tenants>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="classiFication"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="status"></param>
        /// <param name="industryId"></param>
        /// <param name="creatorId"></param>
        /// <param name="creatorTenantId"></param>
        /// <returns></returns>
        public async Task<IPagedList<Tenants>> GetPageList(int page = 1, int limit = 15, string tenantId = "", int classiFication = 0, string saaSVersionId = "", int status = 0, string industryId = "", string creatorId = "", string creatorTenantId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM `Tenants` WHERE `IsDeleted`=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND Id=@Id ");
                    parameters.Add("Id", tenantId);
                }

                if (classiFication > 0)
                {
                    sqlStr.Append(" AND ClassiFication=@ClassiFication ");
                    parameters.Add("ClassiFication", classiFication);
                }

                if (!string.IsNullOrEmpty(saaSVersionId))
                {
                    sqlStr.Append(" AND SaaSVersionId=@SaaSVersionId ");
                    parameters.Add("SaaSVersionId", saaSVersionId);
                }

                if (status > 0)
                {
                    sqlStr.Append(" AND Status=@Status ");
                    parameters.Add("Status", status);
                }

                if (!string.IsNullOrEmpty(industryId))
                {
                    sqlStr.Append(" AND IndustryId=@IndustryId ");
                    parameters.Add("IndustryId", industryId);
                }

                if (!string.IsNullOrEmpty(creatorId))
                {
                    sqlStr.Append(" AND CreatorId=@CreatorId ");
                    parameters.Add("CreatorId", creatorId);
                }

                if (!string.IsNullOrEmpty(creatorTenantId))
                {
                    sqlStr.Append(" AND CreatorTenantId=@CreatorTenantId ");
                    parameters.Add("CreatorTenantId", creatorTenantId);
                }

                var list = await connection.QueryAsync<Tenants>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tenants> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM Tenants WHERE Id=@Id ");

                return await connection.QueryFirstOrDefaultAsync<Tenants>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Tenants> GetModel(Tenants model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM Tenants WHERE Name=@Name ");

                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }

                return await connection.QueryFirstOrDefaultAsync<Tenants>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 租户开通
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="department"></param>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <param name="rolesOwnedModules"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<bool> Add(Tenants tenant, Departments department, Users user, Roles role, IList<RolesOwnedModules> rolesOwnedModules, Orders order)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //写租户信息
                        sqlStr.Append(" INSERT INTO Tenants ");
                        sqlStr.Append(" (Id,Name,Contacts,ContactsPhone,ClassiFication,SaaSVersionId,AppVersionName,Status,ProvinceId,ProvinceName,CityId,CityName,DistrictId,DistrictName,Address,VIPExpirationTime,IndustryId,IndustryName,CreatorId,Creator,CreatorTenantId,CreatorTenantName,IsDeleted,CreateTime) ");
                        sqlStr.Append(" VALUES ");
                        sqlStr.Append(" (@Id,@Name,@Contacts,@ContactsPhone,@ClassiFication,@SaaSVersionId,@AppVersionName,@Status,@ProvinceId,@ProvinceName,@CityId,@CityName,@DistrictId,@DistrictName,@Address,@VIPExpirationTime,@IndustryId,@IndustryName,@CreatorId,@Creator,@CreatorTenantId,@CreatorTenantName,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), tenant, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //创建部门 - 组织机构
                        sqlStr.Append("INSERT INTO Departments (Id,ParentId,`Name`,`Describe`,DisplayOrder,TenantId,IsDeleted,CreateTime) VALUES (@Id,@ParentId,@Name,@Describe,@DisplayOrder,@TenantId,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), department, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //创建角色
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO Roles ");
                        sqlStr.Append(" (Id,Name,NormalizedName,ConcurrencyStamp,Identifying,TenantId,IsDeleted,CreateTime) ");
                        sqlStr.Append(" VALUES ");
                        sqlStr.Append(" (@Id,@Name,@NormalizedName,@ConcurrencyStamp,@Identifying,@TenantId,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), role, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //赋予角色权限
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO RolesOwnedModules ");
                        sqlStr.Append(" (Id,RoleId,ModuleId,TenantId) ");
                        sqlStr.Append(" VALUES ");
                        sqlStr.Append(" (@Id,@RoleId,@ModuleId,@TenantId) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), rolesOwnedModules, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //创建账号
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO Users ");
                        sqlStr.Append(" (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,UserName,Account,PhoneNumber,PasswordHash,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,RoleId,IsActive,TenantId,DepartmentId,Gender,IsDeleted,CreateTime) ");
                        sqlStr.Append(" VALUES");
                        sqlStr.Append(" (@Id,@AccessFailedCount,@ConcurrencyStamp,@Email,@EmailConfirmed,@LockoutEnabled,@LockoutEnd,@NormalizedEmail,@NormalizedUserName,@UserName,@Account,@PhoneNumber,@PasswordHash,@PhoneNumberConfirmed,@SecurityStamp,@TwoFactorEnabled,@RoleId,@IsActive,@TenantId,@DepartmentId,@Gender,@IsDeleted,@CreateTime)");
                        await connection.ExecuteAsync(sqlStr.ToString(), user, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写订单信息
                        sqlStr.Append(" INSERT INTO Orders ");
                        sqlStr.Append(" (Id,TenantId,TenantName,NumberOfYears,PayTheAmount,PerformanceOwnerId,PerformanceOwner,PerformanceTenantId,PerformanceTenant,TradeCategories,IsDeleted,CreateTime) ");
                        sqlStr.Append(" VALUES ");
                        sqlStr.Append(" (@Id,@TenantId,@TenantName,@NumberOfYears,@PayTheAmount,@PerformanceOwnerId,@PerformanceOwner,@PerformanceTenantId,@PerformanceTenant,@TradeCategories,@IsDeleted,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), order, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"机构入驻时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 更新 -> 基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(Tenants model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" UPDATE Tenants SET Contacts=@Contacts,ContactsPhone=@ContactsPhone, ");
                sqlStr.Append(" ProvinceId=@ProvinceId,ProvinceName=@ProvinceName,CityId=@CityId,CityName=@CityName,DistrictId=@DistrictId,DistrictName=@DistrictName, ");
                sqlStr.Append(" Address=@Address WHERE Id=@Id ");

                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新 -> 租户状态 1-正常 2-锁定（异常）3-锁定（过期）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> UpdateStatus(Tenants model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" UPDATE Tenants SET Status=@Status WHERE Id=@Id ");

                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
