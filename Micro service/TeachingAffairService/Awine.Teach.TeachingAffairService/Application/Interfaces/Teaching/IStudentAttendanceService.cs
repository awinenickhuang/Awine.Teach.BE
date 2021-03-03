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
    /// 学生考勤
    /// </summary>
    public interface IStudentAttendanceService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="classId"></param>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <param name="studentName"></param>
        /// <param name="attendanceStatus"></param>
        /// <param name="scheduleIdentification"></param>
        /// <param name="processingStatus"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IPagedList<StudentAttendanceViewModel>> GetPageList(int page = 1, int limit = 15, string classId = "", string courseId = "", string studentId = "", string studentName = "", int attendanceStatus = 0, int scheduleIdentification = 0, int processingStatus = 0, string beginDate = "", string endDate = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentAttendanceViewModel> GetModel(string id);

        /// <summary>
        /// 课节点名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Attendance(SignInViewModel model);

        /// <summary>
        /// 课节点名 -> 出勤情况 -> 一对一试听课节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> TrialClassSigninAttendance(SignInViewModel model);

        /// <summary>
        /// 课节点名 -> 出勤情况 -> 补课课节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> MakeupMissedLessonAttendance(MakeupMissedLessonAttendanceViewModel model);

        /// <summary>
        /// 取消考勤
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> CancelAttendance(string id);

        /// <summary>
        /// 课消金额统计分析
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<BasicBarChartViewModel> AttendanceAmountReport(string date);

        /// <summary>
        /// 课消数量统计分析
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<BasicBarChartViewModel> AttendanceNumberReport(string date);
    }
}
