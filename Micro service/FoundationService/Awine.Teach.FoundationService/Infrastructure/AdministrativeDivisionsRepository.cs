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
    /// 行政区域划分
    /// </summary>
    public class AdministrativeDivisionsRepository : IAdministrativeDivisionsRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<AdministrativeDivisionsRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public AdministrativeDivisionsRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<AdministrativeDivisionsRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AdministrativeDivisions>> GetAll()
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM administrativedivision ORDER BY Code ASC ");
                return await connection.QueryAsync<AdministrativeDivisions>(sqlStr.ToString(), commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 获取下级区域数据
        /// </summary>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AdministrativeDivisions>> GetSubordinateRegionalism(int parentCode)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM administrativedivision WHERE ParentCode=@ParentCode ORDER BY Code ASC ");
                return await connection.QueryAsync<AdministrativeDivisions>(sqlStr.ToString(), new { ParentCode = parentCode }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
