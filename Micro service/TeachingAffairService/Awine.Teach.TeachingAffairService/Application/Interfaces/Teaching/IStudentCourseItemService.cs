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
    /// 学生选课信息
    /// </summary>
    public interface IStudentCourseItemService
    {
        /// <summary>
        /// 所有数据 -> 不分页列表
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="classesId"></param>
        /// <param name="studentOrderId"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        Task<IEnumerable<StudentCourseItemViewModel>> GetAll(string studentId = "", string courseId = "", string classesId = "", string studentOrderId = "", int learningProcess = 0);

        /// <summary>
        /// 所有数据 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="classesId"></param>
        /// <param name="studentOrderId"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        Task<IPagedList<StudentCourseItemViewModel>> GetPageList(int page = 1, int limit = 15, string studentId = "", string courseId = "", string classesId = "", string studentOrderId = "", int learningProcess = 0);

        /// <summary>
        /// 把学生添加进班级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> AddStudentsIntoClass(StudentCourseItemUpdateViewModel model);

        /// <summary>
        /// 把学生从班级中移除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> RemoveStudentFromClass(StudentCourseItemUpdateViewModel model);

        /// <summary>
        /// 更新报读课程的学习进度
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdateLearningProcess(UpdateLearningProcessViewModel model);
    }
}
