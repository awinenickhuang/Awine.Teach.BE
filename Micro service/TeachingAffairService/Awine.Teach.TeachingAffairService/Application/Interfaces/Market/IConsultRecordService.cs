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

        /// <summary>
        /// 生源情况统计 -> 绘图
        /// </summary>
        /// <param name="statisticalMmethod"></param>
        /// <returns></returns>
        Task<IEnumerable<ConsultRecordChartViewModel>> ConsultRecordChartReport(int statisticalMmethod);
    }
}
