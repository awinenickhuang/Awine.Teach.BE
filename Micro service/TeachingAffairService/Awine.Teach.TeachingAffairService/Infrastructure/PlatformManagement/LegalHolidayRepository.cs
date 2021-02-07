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
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Infrastructure.Repository
{
    /// <summary>
    /// 法定假日
    /// </summary>
    public class LegalHolidayRepository : ILegalHolidayRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<LegalHolidayRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public LegalHolidayRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<LegalHolidayRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<IPagedList<LegalHoliday>> GetPageList(int page = 1, int limit = 15, int year = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_pm_legal_holiday WHERE IsDeleted=0 ");

                if (year > 0)
                {
                    sqlStr.Append(" AND Year=@Year ");
                    parameters.Add("Year", year);
                }

                sqlStr.Append(" ORDER BY HolidayDate DESC ");

                var list = await connection.QueryAsync<LegalHoliday>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LegalHoliday>> GetAll(int year = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_pm_legal_holiday WHERE IsDeleted=0 ");

                if (year > 0)
                {
                    sqlStr.Append(" AND Year=@Year ");
                    parameters.Add("Year", year);
                }

                sqlStr.Append(" ORDER BY HolidayDate ");

                return await connection.QueryAsync<LegalHoliday>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<LegalHoliday> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_pm_legal_holiday WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<LegalHoliday>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<LegalHoliday> GetModel(LegalHoliday model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_pm_legal_holiday WHERE HolidayDate=@HolidayDate ");
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }
                return await connection.QueryFirstOrDefaultAsync<LegalHoliday>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(LegalHoliday model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("INSERT INTO t_pm_legal_holiday (Id,Year,`HolidayName`,`HolidayDate`,IsDeleted,CreateTime) VALUES (@Id,@Year,@HolidayName,@HolidayDate,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(LegalHoliday model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE t_pm_legal_holiday SET Year=@Year,`HolidayName`=@HolidayName,HolidayDate=@HolidayDate WHERE Id=@Id ");
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
                string sqlStr = "DELETE FROM t_pm_legal_holiday WHERE Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
