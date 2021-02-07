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
    /// 咨询记录
    /// </summary>
    public class ConsultRecordRepository : IConsultRecordRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<ConsultRecordRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public ConsultRecordRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<ConsultRecordRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="counselingCourseId"></param>
        /// <param name="marketingChannelId"></param>
        /// <param name="trackingState"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ConsultRecord>> GetAll(string tenantId = "", string counselingCourseId = "", string marketingChannelId = "", int trackingState = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();
                sqlStr.Append(" SELECT * FROM t_market_consult_record WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(counselingCourseId))
                {
                    sqlStr.Append(" AND CounselingCourseId=@CounselingCourseId ");
                    parameters.Add("CounselingCourseId", counselingCourseId);
                }

                if (!string.IsNullOrEmpty(marketingChannelId))
                {
                    sqlStr.Append(" AND MarketingChannelId=@MarketingChannelId ");
                    parameters.Add("MarketingChannelId", marketingChannelId);
                }

                if (trackingState > 0)
                {
                    sqlStr.Append(" AND TrackingState=@TrackingState ");
                    parameters.Add("TrackingState", trackingState);
                }

                sqlStr.Append(" ORDER BY CreateTime ");

                return await connection.QueryAsync<ConsultRecord>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="counselingCourseId"></param>
        /// <param name="marketingChannelId"></param>
        /// <param name="trackingState"></param>
        /// <param name="trackingStafferId"></param>
        /// <returns></returns>
        public async Task<IPagedList<ConsultRecord>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string name = "", string phoneNumber = "", string startTime = "", string endTime = "", string counselingCourseId = "", string marketingChannelId = "", int trackingState = 0, string trackingStafferId = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_market_consult_record WHERE IsDeleted=0 ");
                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("TenantId", tenantId);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    sqlStr.Append(" AND Name=@Name ");
                    parameters.Add("Name", name);
                }
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    sqlStr.Append(" AND PhoneNumber=@PhoneNumber ");
                    parameters.Add("PhoneNumber", phoneNumber);
                }
                if (!string.IsNullOrEmpty(startTime))
                {
                    sqlStr.Append(" AND CreateTime>=@StartTime ");
                    parameters.Add("StartTime", startTime);
                }
                if (!string.IsNullOrEmpty(endTime))
                {
                    sqlStr.Append(" AND CreateTime<=@EndTime ");
                    parameters.Add("EndTime", endTime);
                }
                if (!string.IsNullOrEmpty(counselingCourseId))
                {
                    sqlStr.Append(" AND CounselingCourseId=@CounselingCourseId ");
                    parameters.Add("CounselingCourseId", counselingCourseId);
                }
                if (!string.IsNullOrEmpty(marketingChannelId))
                {
                    sqlStr.Append(" AND MarketingChannelId=@MarketingChannelId ");
                    parameters.Add("MarketingChannelId", marketingChannelId);
                }
                if (trackingState > 0)
                {
                    sqlStr.Append(" AND TrackingState=@TrackingState ");
                    parameters.Add("TrackingState", trackingState);
                }
                if (!string.IsNullOrEmpty(trackingStafferId))
                {
                    sqlStr.Append(" AND TrackingStafferId=@TrackingStafferId ");
                    parameters.Add("TrackingStafferId", trackingStafferId);
                }
                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<ConsultRecord>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ConsultRecord> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_market_consult_record WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<ConsultRecord>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ConsultRecord> GetModel(ConsultRecord model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_market_consult_record WHERE (Name=@Name OR PhoneNumber=@PhoneNumber) AND TenantId=@TenantId ");
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }
                return await connection.QueryFirstOrDefaultAsync<ConsultRecord>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(ConsultRecord model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" INSERT INTO t_market_consult_record ");
                sqlStr.Append(" (Id,TenantId,`Name`,Gender,Age,`PhoneNumber`,CounselingCourseId,CounselingCourseName,BasicSituation,TrackingState,TrackingStafferId,TrackingStafferName,MarketingChannelId,MarketingChannelName,ClinchIntentionStar,CreatorId,CreatorName,IsDeleted,CreateTime) ");
                sqlStr.Append(" VALUES");
                sqlStr.Append(" (@Id,@TenantId,@Name,@Gender,@Age,@PhoneNumber,@CounselingCourseId,@CounselingCourseName,@BasicSituation,@TrackingState,@TrackingStafferId,@TrackingStafferName,@MarketingChannelId,@MarketingChannelName,@ClinchIntentionStar,@CreatorId,@CreatorName,@IsDeleted,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(ConsultRecord model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE t_market_consult_record SET Age=@Age,`PhoneNumber`=@PhoneNumber,CounselingCourseId=@CounselingCourseId, ");
                sqlStr.Append(" CounselingCourseName=@CounselingCourseName,BasicSituation=@BasicSituation,");
                sqlStr.Append(" MarketingChannelId=@MarketingChannelId,MarketingChannelName=@MarketingChannelName,ClinchIntentionStar=@ClinchIntentionStar ");
                sqlStr.Append(" WHERE Id=@Id ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 跟进任务指派 -> 同时更新跟进状态为 ->跟进中
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> TrackingAssigned(ConsultRecord model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE t_market_consult_record SET `TrackingStafferId`=@TrackingStafferId,TrackingStafferName=@TrackingStafferName,TrackingState=2 WHERE Id=@Id ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
