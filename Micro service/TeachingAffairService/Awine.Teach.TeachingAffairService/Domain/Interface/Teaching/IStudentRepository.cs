using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 学生
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// 所有学生数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IEnumerable<Student>> GetAll(string tenantId);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        Task<IPagedList<Student>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string name = "", int gender = 0, string phoneNumber = "", int learningProcess = 0);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Student> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Student> GetModel(Student model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Task<int> Add(Student student);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(Student model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);

        /// <summary>
        /// 学生报名 -> 新生报名
        /// </summary>
        /// <param name="student"></param>
        /// <param name="studentCourseItem"></param>
        /// <param name="studentCourseOrder"></param>
        /// <returns></returns>
        Task<bool> Registration(Student student, StudentCourseItem studentCourseItem, StudentCourseOrder studentCourseOrder);

        /// <summary>
        /// 学生报名 -> 学生扩科
        /// </summary>
        /// <param name="studentCourseItem"></param>
        /// <param name="studentCourseOrder"></param>
        /// <returns></returns>
        Task<bool> IncreaseLearningCourses(StudentCourseItem studentCourseItem, StudentCourseOrder studentCourseOrder);

        /// <summary>
        /// 学生报名 -> 缴费续费
        /// </summary>
        /// <param name="studentCourseItem"></param>
        /// <param name="studentCourseOrder"></param>
        /// <returns></returns>
        Task<bool> ContinueTopaytuition(StudentCourseItem studentCourseItem, StudentCourseOrder studentCourseOrder);
    }
}
