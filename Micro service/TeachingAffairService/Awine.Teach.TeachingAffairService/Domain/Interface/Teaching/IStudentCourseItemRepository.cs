using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 学生选课信息
    /// </summary>
    public interface IStudentCourseItemRepository
    {
        /// <summary>
        /// 所有数据 -> 不分页列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="classesId"></param>
        /// <param name="studentOrderId"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        Task<IEnumerable<StudentCourseItem>> GetAll(string tenantId = "", string studentId = "", string courseId = "", string classesId = "", string studentOrderId = "", int learningProcess = 0);

        /// <summary>
        /// 所有数据 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="classesId"></param>
        /// <param name="studentOrderId"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        Task<IPagedList<StudentCourseItem>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string studentId = "", string courseId = "", string classesId = "", string studentOrderId = "", int learningProcess = 0);

        /// <summary>
        /// 更新学生的就读班级信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="clssses"></param>
        /// <returns></returns>
        Task<bool> UpdateStudentsClassInformation(List<StudentCourseItem> model, Classes clssses);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentCourseItem> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="chargeManner"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        Task<StudentCourseItem> GetModel(string tenantId, string studentId, string courseId, int chargeManner, int learningProcess);

        /// <summary>
        /// 更新报读课程的学习进度
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateLearningProcess(StudentCourseItem model);
    }
}
