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
    /// 课程
    /// </summary>
    public interface ICourseService
    {
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CourseViewModel>> GetAll(int enabledStatus = 0);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="enabledStatus"></param>
        /// <returns></returns>
        Task<IPagedList<CourseViewModel>> GetPageList(int page = 1, int limit = 15, int enabledStatus = 0);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(CourseAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(CourseUpdateViewModel model);

        /// <summary>
        /// 更新 -> 课程状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdateEnableStatus(CourseUpdateEnableStatusViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
