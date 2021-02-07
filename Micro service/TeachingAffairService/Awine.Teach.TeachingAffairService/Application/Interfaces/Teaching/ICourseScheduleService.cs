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
    /// 班级 -> 排课信息
    /// </summary>
    public interface ICourseScheduleService
    {
        /// <summary>
        /// 排课信息 -> 所有数据 -> 用于初始化课表日历 -> 按天统计（每天排了多少节课）
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="courseDates"></param>
        /// <param name="classStatus">课节状态 1-待上课 2-已结课</param>
        /// <returns></returns>
        Task<IEnumerable<CourseScheduleStatisticalViewModel>> GetStatisticsDaily(string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", string courseDates = "", int classStatus = 0);

        /// <summary>
        /// 排课信息 -> 所有数据 -> 用于初始化课表日历 -> 携带日历组件需要的信息
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="courseDates"></param>
        /// <param name="classStatus">课节状态 1-待上课 2-已结课</param>
        /// <returns></returns>
        Task<IEnumerable<CourseScheduleCalendarViewModel>> GetAll(string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", string courseDates = "", int classStatus = 0);

        /// <summary>
        /// 排课信息 -> 所有数据 ->用于查询课程明细
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="courseDates"></param>
        /// <param name="classStatus"></param>
        /// <returns></returns>
        Task<IEnumerable<CourseScheduleViewModel>> GetAllSchedule(string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", string courseDates = "", int classStatus = 0);

        /// <summary>
        /// 排课信息 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="classStatus"></param>
        /// <param name="scheduleIdentification"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IPagedList<CourseScheduleViewModel>> GetPageList(int page = 1, int limit = 15, string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", int classStatus = 0, int scheduleIdentification = 0, string beginDate = "", string endDate = "");

        /// <summary>
        /// 生成排课计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> AddClassSchedulingPlans(CourseScheduleAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(CourseScheduleUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
