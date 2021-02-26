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
    /// 试听管理
    /// </summary>
    public interface ITrialClassService
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="listeningState"></param>
        /// <param name="courseScheduleId"></param>
        /// <returns></returns>
        Task<IEnumerable<TrialClassViewModel>> GetAll(int listeningState = 0, string courseScheduleId = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="listeningState"></param>
        /// <param name="teacherId"></param>
        /// <param name="courseId"></param>
        /// <param name="courseScheduleId"></param>
        /// <returns></returns>
        Task<IPagedList<TrialClassViewModel>> GetPageList(int page = 1, int limit = 15, string name = "", string phoneNumber = "", int listeningState = 0, string teacherId = "", string courseId = "", string courseScheduleId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TrialClassViewModel> GetModel(string id);

        /// <summary>
        /// 跟班试听
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> ListenFollowClass(TrialClassListenViewModel model);

        /// <summary>
        /// 一对一试听
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> OneToOneListen(TrialClassListenCourseViewModel model);

        /// <summary>
        /// 更新 -> 到课状态 -> 单个
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdateListeningState(TrialClassUpdateListeningStateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);

        /// <summary>
        /// 试听课程情况 -> 当月每天的试所有课程试听情况
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        Task<StackedAreaChartViewModel> TrialClassReportChart(string designatedMonth);
    }
}
