using Awine.Framework.AspNetCore.Model;
using Awine.Framework.Core.Collections;
using Awine.Teach.TeachingAffairService.Application.ServiceResult;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Interfaces
{
    /// <summary>
    /// 咨询记录 
    /// </summary>
    public interface IConsultRecordService
    {
        /// <summary>
        /// 机构首页 -> 生源情况统计
        /// </summary>
        /// <returns></returns>
        Task<ConsultRecordStatisticalViewModel> ConsulRecordStatistical();

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="counselingCourseId"></param>
        /// <param name="marketingChannelId"></param>
        /// <param name="trackingState"></param>
        /// <param name="trackingStafferId"></param>
        /// <returns></returns>
        Task<IPagedList<ConsultRecordViewModel>> GetPageList(int page = 1, int limit = 15, string name = "", string phoneNumber = "", string startTime = "", string endTime = "", string counselingCourseId = "", string marketingChannelId = "", int trackingState = 0, string trackingStafferId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ConsultRecordViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(ConsultRecordAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(ConsultRecordUpdateViewModel model);

        /// <summary>
        /// 跟进任务指派
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> TrackingAssigned(ConsultRecordTrackingAssignedViewModel model);

        #region 数据统计

        /// <summary>
        /// 生源情况统计 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        Task<BasicLineChartViewModel> ConsultRecordMonthChartReport(string designatedMonth);

        /// <summary>
        /// 课程咨询情况 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 各课程咨询数量
        /// </remarks>
        Task<PieChartViewModel> ConsultRecordCourseMonthChartReport(string designatedMonth);

        /// <summary>
        /// 营销转化情况 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 各课程咨询数量
        /// </remarks>
        Task<HorizontalBarChartViewModel> StudentTransformationtReport(string designatedMonth);

        /// <summary>
        /// 各营销渠道生源来源情况 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 各招生渠道指定月份的新增生源数量
        /// </remarks>
        Task<StackedLineChartViewModel> StudentSourceChannelReport(string designatedMonth);

        #endregion
    }
}
