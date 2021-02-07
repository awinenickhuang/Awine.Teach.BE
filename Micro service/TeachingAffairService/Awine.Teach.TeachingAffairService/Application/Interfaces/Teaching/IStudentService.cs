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
    /// 学生 
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// 所有学生数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StudentViewModel>> GetAll();

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        Task<IPagedList<StudentViewModel>> GetPageList(int page = 1, int limit = 15, string name = "", int gender = 0, string phoneNumber = "", int learningProcess = 0);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(StudentAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(StudentUpdateViewModel model);

        /// <summary>
        /// 取一个学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentViewModel> GetModel(string id);

        /// <summary>
        /// 学生报名 -> 新生报名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Registration(StudentRegistrationViewModel model);

        /// <summary>
        /// 学生报名 -> 学生扩科
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> IncreaseLearningCourses(StudentIncreaseLearningCoursesViewModel model);

        /// <summary>
        /// 学生报名 -> 缴费续费
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> ContinueTopaytuition(StudentSupplementViewModel model);
    }
}
