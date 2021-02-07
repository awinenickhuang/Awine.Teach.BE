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
    /// 系统账号
    /// </summary>
    public class UsersRepository : IUsersRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<UsersRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public UsersRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<UsersRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="isActive"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Users>> GetAll(string tenantId, bool isActive = true, int gender = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM users WHERE IsDeleted=0 AND TenantId=@TenantId ");
                parameters.Add("TenantId", tenantId);

                sqlStr.Append(" AND IsActive=@IsActive ");
                parameters.Add("IsActive", isActive);

                if (gender > 0)
                {
                    sqlStr.Append(" AND Gender=@Gender ");
                    parameters.Add("Gender", gender);
                }

                return await connection.QueryAsync<Users>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="tenantId"></param>
        /// <param name="departmentId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IPagedList<Users>> GetPageList(int pageIndex = 1, int pageSize = 15, string userName = "", string phoneNumber = "", string tenantId = "", string departmentId = "", string roleId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM users WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(userName))
                {
                    sqlStr.Append(" AND UserName=@UserName ");
                    parameters.Add("UserName", userName);
                }

                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    sqlStr.Append(" AND PhoneNumber=@PhoneNumber ");
                    parameters.Add("PhoneNumber", phoneNumber);
                }

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(departmentId))
                {
                    sqlStr.Append(" AND DepartmentId=@DepartmentId ");
                    parameters.Add("DepartmentId", departmentId);
                }

                if (!string.IsNullOrEmpty(roleId))
                {
                    sqlStr.Append(" AND RoleId=@RoleId ");
                    parameters.Add("RoleId", roleId);
                }

                var users = await connection.QueryAsync<Users>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
                if (null != users)
                {
                    foreach (var item in users)
                    {
                        var tenant = await connection.QueryFirstOrDefaultAsync<Tenants>("select * from tenants where Id = @Id", new { Id = item.TenantId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
                        if (tenant != null)
                        {
                            item.PlatformTenant = tenant;
                        }

                        var aspnetrole = await connection.QueryFirstOrDefaultAsync<Roles>("select * from roles where Id = @Id", new { Id = item.RoleId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
                        if (aspnetrole != null)
                        {
                            item.AspnetRole = aspnetrole;
                        }

                        var departments = await connection.QueryFirstOrDefaultAsync<Departments>("select * from departments where Id = @Id", new { Id = item.DepartmentId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
                        if (aspnetrole != null)
                        {
                            item.Department = departments;
                        }
                    }
                }
                return users.ToPagedList(pageIndex, pageSize);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(Users model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" INSERT INTO users ");
                sqlStr.Append(" (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,UserName,Account,PhoneNumber,PasswordHash,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,RoleId,IsActive,TenantId,DepartmentId,Gender,IsDeleted,CreateTime) ");
                sqlStr.Append(" VALUES");
                sqlStr.Append(" (@Id,@AccessFailedCount,@ConcurrencyStamp,@Email,@EmailConfirmed,@LockoutEnabled,@LockoutEnd,@NormalizedEmail,@NormalizedUserName,@UserName,@Account,@PhoneNumber,@PasswordHash,@PhoneNumberConfirmed,@SecurityStamp,@TwoFactorEnabled,@RoleId,@IsActive,@TenantId,@DepartmentId,@Gender,@IsDeleted,@CreateTime)");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Users> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM users WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<Users>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Users> GetDetailModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();
                sqlStr.Append(" SELECT user.Id,user.UserName,user.Account,user.PhoneNumber,user.Gender,tenant.Id,tenant.Name,tenant.ClassiFication,tenant.VIPExpirationTime,tenant.IndustryName,role.Id,role.Name,departments.Id,departments.Name");
                sqlStr.Append(" FROM users as user INNER JOIN tenants as tenant ON user.TenantId=tenant.Id ");
                sqlStr.Append(" INNER JOIN roles as role on user.RoleId=role.Id ");
                sqlStr.Append(" INNER JOIN departments as departments on user.DepartmentId=departments.Id ");
                sqlStr.Append(" WHERE user.Id=@Id");
                parameters.Add("Id", id);
                var result = await connection.QueryAsync<Users, Tenants, Roles, Departments, Users>(sqlStr.ToString(), (user, tenant, role, departments) =>
                {
                    user.PlatformTenant = tenant;
                    user.AspnetRole = role;
                    user.Department = departments;
                    return user;
                },
                parameters,
                splitOn: "Id",
                commandTimeout: _mySQLProviderOptions.CommandTimeOut,
                commandType: CommandType.Text);
                return result?.FirstOrDefault();
            }
        }

        /// <summary>
        /// 取一条数据 -> 全局查询 ->账号或手机号不能重复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Users> GetGlobalModel(Users model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" SELECT * FROM users WHERE Id !=@Id AND (Account=@Account or PhoneNumber=@PhoneNumber)");
                }
                else
                {
                    sqlStr.Append(" SELECT * FROM users WHERE (Account=@Account OR PhoneNumber=@PhoneNumber)");
                }
                return await connection.QueryFirstOrDefaultAsync<Users>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据 -> 当前租户查询 -> 姓名不能重复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Users> GetModel(Users model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" SELECT * FROM users WHERE Id !=@Id AND UserName=@UserName AND TenantId=@TenantId");
                }
                else
                {
                    sqlStr.Append(" SELECT * FROM users WHERE UserName=@UserName AND TenantId=@TenantId");
                }
                return await connection.QueryFirstOrDefaultAsync<Users>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(Users model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE users SET UserName=@UserName,PhoneNumber=@PhoneNumber,Email=@Email,RoleId=@RoleId,Gender=@Gender,DepartmentId=@DepartmentId WHERE Id=@Id ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<int> ChangePassword(string id, string password)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE users SET PasswordHash=@PasswordHash WHERE Id=@Id");
                return await connection.ExecuteAsync(sqlStr.ToString(), new { PasswordHash = password, Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 启用或禁用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> ChangeActive(Users model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                var lockoutEndTime = DateTime.Now.AddDays(-1);
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" UPDATE users SET IsActive=@IsActive WHERE Id=@Id");

                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 查询某一部门的所有用户
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="isActive"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Users>> GetAllInDepartment(string departmentId, bool isActive = true, int gender = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM users WHERE IsDeleted=0 AND DepartmentId=@DepartmentId ");
                parameters.Add("DepartmentId", departmentId);

                sqlStr.Append(" AND IsActive=@IsActive ");
                parameters.Add("IsActive", isActive);

                if (gender > 0)
                {
                    sqlStr.Append(" AND Gender=@Gender ");
                    parameters.Add("Gender", gender);
                }

                return await connection.QueryAsync<Users>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
