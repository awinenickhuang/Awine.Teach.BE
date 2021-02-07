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
    /// 补课管理
    /// </summary>
    public interface IMakeupMissedLessonService
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="status"></param>
        /// <param name="courseId"></param>
        /// <param name="teacherId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IEnumerable<MakeupMissedLessonViewModel>> GetAll(int status = 0, string courseId = "", string teacherId = "", string beginDate = "", string endDate = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="status"></param>
        /// <param name="courseId"></param>
        /// <param name="teacherId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IPagedList<MakeupMissedLessonViewModel>> GetPageList(int page = 1, int limit = 15, int status = 0, string courseId = "", string teacherId = "", string beginDate = "", string endDate = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MakeupMissedLessonViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(MakeupMissedLessonAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(MakeupMissedLessonUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);

        /// <summary>
        /// 补课学生分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="makeupMissedLessonId">补课班级ID</param>
        /// <param name="classCourseScheduleId">补课班级课表ID</param>
        /// <returns></returns>
        Task<IPagedList<MakeupMissedLessonStudentViewModel>> GetMakeupMissedLessonStudentPageList(int page = 1, int limit = 15, string makeupMissedLessonId = "", string classCourseScheduleId = "");

        /// <summary>
        /// 添加补课学生
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> AddStudentToClass(MakeupMissedLessonStudentAddViewModel model);

        /// <summary>
        /// 移除补课学生
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> RemoveStudentFromClass(MakeupMissedLessonStudentRemoveViewModel model);
    }
}
