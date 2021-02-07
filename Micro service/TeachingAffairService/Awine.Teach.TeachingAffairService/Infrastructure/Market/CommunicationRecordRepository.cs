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
    /// 咨询记录 -> 跟进记录
    /// </summary>
    public class CommunicationRecordRepository : ICommunicationRecordRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<CommunicationRecordRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public CommunicationRecordRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<CommunicationRecordRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="consultRecordId"></param>
        /// <returns></returns>
        public async Task<IPagedList<CommunicationRecord>> GetPageList(int page = 1, int limit = 15, string consultRecordId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_market_communication_record WHERE ConsultRecordId=@ConsultRecordId ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<CommunicationRecord>(sqlStr.ToString(), new { ConsultRecordId = consultRecordId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trackingState"></param>
        /// <returns></returns>
        public async Task<bool> Add(CommunicationRecord model, int trackingState)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //添加更进记录
                        sqlStr.Append("INSERT INTO t_market_communication_record (Id,ConsultRecordId,`CommunicationContent`,`TrackingStafferId`,TrackingStafferName,ClinchIntentionStar,CommunicateWay,CreateTime) VALUES (@Id,@ConsultRecordId,@CommunicationContent,@TrackingStafferId,@TrackingStafferName,@ClinchIntentionStar,@CommunicateWay,@CreateTime) ");
                        await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新咨询记录的跟进状态
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE `t_market_consult_record` SET TrackingState=@TrackingState WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { TrackingState = trackingState, Id = model.ConsultRecordId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"添加更进记录时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
