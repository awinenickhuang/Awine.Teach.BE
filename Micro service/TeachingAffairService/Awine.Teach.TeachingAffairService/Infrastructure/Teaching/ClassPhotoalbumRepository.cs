using Awine.Framework.Core.Collections;
using Awine.Framework.Dapper.Extensions.Options;
using Awine.Teach.TeachingAffairService.Domain;
using Awine.Teach.TeachingAffairService.Domain.Interface;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Infrastructure.Repository
{
    /// <summary>
    /// 班级相册
    /// </summary>
    public class ClassPhotoalbumRepository : IClassPhotoalbumRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<ClassPhotoalbumRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public ClassPhotoalbumRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<ClassPhotoalbumRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="classId"></param>
        /// <param name="visibleRange"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ClassPhotoalbum>> GetAll(string tenantId = "", string classId = "", int visibleRange = 0, string startDate = "", string endDate = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_class_photoalbum WHERE IsDeleted=0 ");
                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }
                if (!string.IsNullOrEmpty(classId))
                {
                    sqlStr.Append(" AND ClassId=@ClassId ");
                    parameters.Add("ClassId", classId);
                }
                if (visibleRange > 0)
                {
                    sqlStr.Append(" AND VisibleRange=@VisibleRange ");
                    parameters.Add("VisibleRange", visibleRange);
                }
                if (!string.IsNullOrEmpty(startDate))
                {
                    sqlStr.Append(" AND CreateTime>=@StartDate ");
                    parameters.Add("StartDate", startDate);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    sqlStr.Append(" AND CreateTime<=@EndDate ");
                    parameters.Add("EndDate", endDate);
                }
                sqlStr.Append(" ORDER BY StartDate ");
                return await connection.QueryAsync<ClassPhotoalbum>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="classId"></param>
        /// <param name="visibleRange"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPagedList<ClassPhotoalbum>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string classId = "", int visibleRange = 0, string startDate = "", string endDate = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM t_ea_class_photoalbum WHERE IsDeleted=0 ");
                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }
                if (!string.IsNullOrEmpty(classId))
                {
                    sqlStr.Append(" AND ClassId=@ClassId ");
                    parameters.Add("ClassId", classId);
                }
                if (visibleRange > 0)
                {
                    sqlStr.Append(" AND VisibleRange=@VisibleRange ");
                    parameters.Add("VisibleRange", visibleRange);
                }
                if (!string.IsNullOrEmpty(startDate))
                {
                    sqlStr.Append(" AND CreateTime>=@StartDate ");
                    parameters.Add("StartDate", startDate);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    sqlStr.Append(" AND CreateTime<=@EndDate ");
                    parameters.Add("EndDate", endDate);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<ClassPhotoalbum>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(ClassPhotoalbum model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" INSERT INTO t_ea_class_photoalbum (Id,ClassId,`Name`,`CoverPhoto`,`Describe`,`VisibleRange`,TenantId,IsDeleted,CreateTime) ");
                sqlStr.Append(" VALUES ");
                sqlStr.Append(" (@Id,@ClassId,@Name,@CoverPhoto,@Describe,@VisibleRange,@TenantId,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ClassPhotoalbum> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_class_photoalbum WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<ClassPhotoalbum>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ClassPhotoalbum> GetModel(ClassPhotoalbum model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_class_photoalbum WHERE Name=@Name AND TenantId=@TenantId ");
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }
                return await connection.QueryFirstOrDefaultAsync<ClassPhotoalbum>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(ClassPhotoalbum model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE t_ea_class_photoalbum SET `Name`=@Name,CoverPhoto=@CoverPhoto,Describe=@Describe,VisibleRange=@VisibleRange WHERE Id=@Id ");
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

                        sqlStr.Clear();
                        sqlStr.Append(" DELETE FROM t_ea_class_photoalbum_attachment WHERE PhotoalbumId=@PhotoalbumId ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { PhotoalbumId = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        sqlStr.Clear();
                        sqlStr.Append(" DELETE FROM t_ea_class_photoalbum WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"删除相册时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
