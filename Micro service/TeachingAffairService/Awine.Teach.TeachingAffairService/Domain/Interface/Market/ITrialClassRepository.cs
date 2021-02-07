using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 试听管理
    /// </summary>
    public interface ITrialClassRepository
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="listeningState"></param>
        /// <param name="courseScheduleId"></param>
        /// <returns></returns>
        Task<IEnumerable<TrialClass>> GetAll(string tenantId = "", int listeningState = 0, string courseScheduleId = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="listeningState"></param>
        /// <param name="teacherId"></param>
        /// <param name="courseId"></param>
        /// <param name="courseScheduleId"></param>
        /// <returns></returns>
        Task<IPagedList<TrialClass>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string name = "", string phoneNumber = "", int listeningState = 0, string teacherId = "", string courseId = "", string courseScheduleId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TrialClass> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TrialClass> GetModel(TrialClass model);

        /// <summary>
        /// 跟班试听
        /// </summary>
        /// <param name="trialClass"></param>
        /// <returns></returns>
        Task<int> ListenFollowClass(TrialClass trialClass);

        /// <summary>
        /// 一对一试听
        /// </summary>
        /// <param name="trialClass"></param>
        /// <param name="courseSchedules"></param>
        /// <returns></returns>
        Task<bool> OneToOneListen(TrialClass trialClass, CourseSchedule courseSchedules);

        /// <summary>
        /// 更新 -> 到课状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateListeningState(TrialClass model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Delete(TrialClass model);
    }
}
