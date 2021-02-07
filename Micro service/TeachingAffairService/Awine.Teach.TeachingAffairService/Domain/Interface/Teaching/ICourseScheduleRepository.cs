﻿using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 班级 -> 排课信息
    /// </summary>
    public interface ICourseScheduleRepository
    {
        /// <summary>
        /// 排课信息 -> 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="courseDates"></param>
        /// <param name="classStatus">课节状态 1-待上课 2-已结课</param>
        /// <returns></returns>
        Task<IEnumerable<CourseSchedule>> GetAll(string tenantId = "", string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", string courseDates = "", int classStatus = 0);

        /// <summary>
        /// 排课信息 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="courseId"></param>
        /// <param name="classId"></param>
        /// <param name="teacherId"></param>
        /// <param name="classRoomId"></param>
        /// <param name="classStatus"></param>
        /// <param name=""></param>
        /// <param name="scheduleIdentification"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IPagedList<CourseSchedule>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string courseId = "", string classId = "", string teacherId = "", string classRoomId = "", int classStatus = 0, int scheduleIdentification = 0, string beginDate = "", string endDate = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CourseSchedule> GetModel(string id);

        /// <summary>
        /// 添加排课计划
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<int> AddClassSchedulingPlans(IList<CourseSchedule> models);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(CourseSchedule model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
