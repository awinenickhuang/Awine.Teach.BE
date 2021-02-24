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
        /// <returns></returns>
        public async Task<IEnumerable<Tenants>> GetAll(string tenantId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM `tenants` WHERE `ParentId`=@ParentId OR `Id`=@Id ");

                parameters.Add("ParentId", tenantId);
                parameters.Add("Id", tenantId);

                return await connection.QueryAsync<Tenants>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IPagedList<Tenants>> GetPageList(int page = 1, int limit = 15, string tenantId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM `tenants` WHERE `ParentId`=@ParentId OR `Id`=@Id ");

                parameters.Add("ParentId", tenantId);
                parameters.Add("Id", tenantId);

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

                sqlStr.Append(" SELECT * FROM tenants WHERE Id=@Id ");

                return await connection.QueryFirstOrDefaultAsync<Tenants>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取某一类型租户
        /// </summary>
        /// <param name="classiFication"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Tenants>> GetClassiFication(int classiFication)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM tenants WHERE ClassiFication=@ClassiFication ");

                return await connection.QueryAsync<Tenants>(sqlStr.ToString(), new { ClassiFication = classiFication }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
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

                sqlStr.Append(" SELECT * FROM tenants WHERE Name=@Name ");

                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }

                return await connection.QueryFirstOrDefaultAsync<Tenants>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(Tenants model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" INSERT INTO tenants ");
                sqlStr.Append(" (Id,ParentId,Name,Contacts,ContactsPhone,ClassiFication,Status,ProvinceId,ProvinceName,CityId,CityName,DistrictId,DistrictName,Address,VIPExpirationTime,IndustryId,IndustryName,NumberOfBranches,CreateTime) ");
                sqlStr.Append(" VALUES ");
                sqlStr.Append(" (@Id,@ParentId,@Name,@Contacts,@ContactsPhone,@ClassiFication,@Status,@ProvinceId,@ProvinceName,@CityId,@CityName,@DistrictId,@DistrictName,@Address,@VIPExpirationTime,@IndustryId,@IndustryName,@NumberOfBranches,@CreateTime) ");

                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
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

                sqlStr.Append(" UPDATE tenants SET Contacts=@Contacts,ContactsPhone=@ContactsPhone, ");
                sqlStr.Append(" ProvinceId=@ProvinceId,ProvinceName=@ProvinceName,CityId=@CityId,CityName=@CityName,DistrictId=@DistrictId,DistrictName=@DistrictName, ");
                sqlStr.Append(" Address=@Address WHERE Id=@Id ");

                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新 -> 租户类型 1-免费 2-试用 3-付费（VIP）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> UpdateClassiFication(Tenants model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE tenants SET ClassiFication=@ClassiFication WHERE Id=@Id");
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

                sqlStr.Append(" UPDATE tenants SET Status=@Status WHERE Id=@Id ");

                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新 -> 允许添加的分支机构个数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> UpdateNumberOfBranches(Tenants model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" UPDATE tenants SET NumberOfBranches=@NumberOfBranches WHERE Id=@Id ");

                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 入驻 -> 注册
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<bool> Enter(Tenants tenantModel, Users userModel)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //写入租户
                        sqlStr.Append(" INSERT INTO tenants ");
                        sqlStr.Append(" (Id,ParentId,Name,Contacts,ContactsPhone,ClassiFication,Status,ProvinceId,ProvinceName,CityId,CityName,DistrictId,DistrictName,Address,VIPExpirationTime,IndustryId,IndustryName,NumberOfBranches,CreateTime) ");
                        sqlStr.Append(" VALUES ");
                        sqlStr.Append(" (@Id,@ParentId,@Name,@Contacts,@ContactsPhone,@ClassiFication,@Status,@ProvinceId,@ProvinceName,@CityId,@CityName,@DistrictId,@DistrictName,@Address,@VIPExpirationTime,@IndustryId,@IndustryName,@NumberOfBranches,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), tenantModel, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //创建账号
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO users ");
                        sqlStr.Append(" (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,UserName,Account,PhoneNumber,PasswordHash,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,RoleId,IsActive,TenantId,DepartmentId,Gender,IsDeleted,CreateTime) ");
                        sqlStr.Append(" VALUES");
                        sqlStr.Append(" (@Id,@AccessFailedCount,@ConcurrencyStamp,@Email,@EmailConfirmed,@LockoutEnabled,@LockoutEnd,@NormalizedEmail,@NormalizedUserName,@UserName,@Account,@PhoneNumber,@PasswordHash,@PhoneNumberConfirmed,@SecurityStamp,@TwoFactorEnabled,@RoleId,@IsActive,@TenantId,@DepartmentId,@Gender,@IsDeleted,@CreateTime)");
                        await connection.ExecuteAsync(sqlStr.ToString(), userModel, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"入驻 -> 注册 时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
