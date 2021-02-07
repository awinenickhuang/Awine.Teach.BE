using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 补课管理
    /// </summary>
    public interface IMakeupMissedLessonRepository
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="status"></param>
        /// <param name="courseId"></param>
        /// <param name="teacherId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IEnumerable<MakeupMissedLesson>> GetAll(string tenantId = "", int status = 0, string courseId = "", string teacherId = "", string beginDate = "", string endDate = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="status"></param>
        /// <param name="courseId"></param>
        /// <param name="teacherId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IPagedList<MakeupMissedLesson>> GetPageList(int page = 1, int limit = 15, string tenantId = "", int status = 0, string courseId = "", string teacherId = "", string beginDate = "", string endDate = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MakeupMissedLesson> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Add(MakeupMissedLesson model, CourseSchedule courseSchedule);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="courseSchedule"></param>
        /// <returns></returns>
        Task<bool> Update(MakeupMissedLesson model, CourseSchedule courseSchedule);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string id);

        /// <summary>
        /// 补课学生分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="makeupMissedLessonId">补课班级ID</param>
        /// <param name="classCourseScheduleId">补课班级课表ID</param>
        /// <returns></returns>
        Task<IPagedList<MakeupMissedLessonStudent>> GetMakeupMissedLessonStudentPageList(int page = 1, int limit = 15, string tenantId = "", string makeupMissedLessonId = "", string classCourseScheduleId = "");

        /// <summary>
        /// 添加补课学生
        /// </summary>
        /// <param name="students"></param>
        /// <param name="studentAttendances"></param>
        /// <returns></returns>
        Task<bool> AddStudentToClass(IList<MakeupMissedLessonStudent> students, IList<StudentAttendance> studentAttendances);

        /// <summary>
        /// 移除补课学生
        /// </summary>
        /// <param name="students"></param>
        /// <param name="studentAttendances"></param>
        /// <returns></returns>
        Task<bool> RemoveStudentFromClass(IList<MakeupMissedLessonStudent> students, IList<StudentAttendance> studentAttendances);
    }
}
