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
    /// 课程收费方式
    /// </summary>
    public interface ICourseChargeMannerService
    {
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<IEnumerable<CourseChargeMannerViewModel>> GetAll(string courseId = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<IPagedList<CourseChargeMannerViewModel>> GetPageList(int page = 1, int limit = 15, string courseId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CourseChargeMannerViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(CourseChargeMannerAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(CourseChargeMannerUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
