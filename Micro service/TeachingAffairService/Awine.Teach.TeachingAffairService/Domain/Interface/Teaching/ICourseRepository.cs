using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 课程
    /// </summary>
    public interface ICourseRepository
    {
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="enabledStatus"></param>
        /// <returns></returns>
        Task<IEnumerable<Course>> GetAll(string tenantId = "", int enabledStatus = 0);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="enabledStatus"></param>
        /// <returns></returns>
        Task<IPagedList<Course>> GetPageList(int page = 1, int limit = 15, string tenantId = "", int enabledStatus = 0);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Course> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Course> GetModel(Course model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(Course model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(Course model);

        /// <summary>
        /// 更新 -> 课程状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateEnableStatus(Course model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
