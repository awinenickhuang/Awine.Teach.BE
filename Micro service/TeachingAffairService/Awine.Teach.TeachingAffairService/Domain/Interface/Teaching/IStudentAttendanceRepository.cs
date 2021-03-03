using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 学生考勤
    /// </summary>
    public interface IStudentAttendanceRepository
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
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
        Task<IEnumerable<StudentAttendance>> GetAll(string tenantId = "", string classId = "", string courseId = "", string studentId = "", string studentName = "", int attendanceStatus = 0, int scheduleIdentification = 0, int processingStatus = 0, string beginDate = "", string endDate = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
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
        Task<IPagedList<StudentAttendance>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string classId = "", string courseId = "", string studentId = "", string studentName = "", int attendanceStatus = 0, int scheduleIdentification = 0, int processingStatus = 0, string beginDate = "", string endDate = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentAttendance> GetModel(string id);

        /// <summary>
        ///  课节点名 -> 出勤情况 -> 正式课节 -> 包括在读学生 跟班试听学生
        /// </summary>
        /// <param name="classCourseSchedule"></param>
        /// <param name="studentCourseItems"></param>
        /// <param name="studentAttendances"></param>
        /// <param name="trialClasses"></param>
        /// <returns></returns>
        Task<bool> Attendance(CourseSchedule classCourseSchedule, IList<StudentCourseItem> studentCourseItems, IList<StudentAttendance> studentAttendances, IList<TrialClass> trialClasses);

        /// <summary>
        /// 课节点名 -> 出勤情况 -> 一对一试听课节
        /// </summary>
        /// <param name="classCourseSchedule">课节息</param>
        /// <param name="trialClasses">试听课节</param>
        /// <returns></returns>
        Task<bool> TrialClassSigninAttendance(CourseSchedule classCourseSchedule, IList<TrialClass> trialClasses);

        /// <summary>
        /// 课节点名 -> 出勤情况 -> 补课课节
        /// </summary>
        /// <param name="classCourseSchedule">课节息</param>
        /// <param name="studentCourseItems">报读课程</param>
        /// <param name="studentAttendances">出勤信息</param>
        /// <param name="makeupMissedLessonStudentAttendances">出勤信息</param>
        /// <param name="makeupMissedLesson">补课班级</param>
        /// <returns></returns>
        Task<bool> MakeupMissedLessonAttendance(CourseSchedule classCourseSchedule, IList<StudentCourseItem> studentCourseItems, IList<StudentAttendance> studentAttendances, IList<StudentAttendance> makeupMissedLessonStudentAttendances, MakeupMissedLesson makeupMissedLesson);
    }
}
