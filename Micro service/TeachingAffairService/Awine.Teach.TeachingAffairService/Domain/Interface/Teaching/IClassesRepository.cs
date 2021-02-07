using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 班级管理
    /// </summary>
    public interface IClassesRepository
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="courseId"></param>
        /// <param name="recruitStatus"></param>
        /// <param name="typeOfClass"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        Task<IEnumerable<Classes>> GetAll(string tenantId = "", string courseId = "", int recruitStatus = 0, int typeOfClass = 0, string beginDate = "", string finishDate = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="name"></param>
        /// <param name="courseId"></param>
        /// <param name="recruitStatus"></param>
        /// <param name="typeOfClass"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        Task<IPagedList<Classes>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string name = "", string courseId = "", int recruitStatus = 0, int typeOfClass = 0, string beginDate = "", string finishDate = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Classes> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Classes> GetModel(Classes model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(Classes model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(Classes model);

        /// <summary>
        /// 更新 -> 班级招生状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateRecruitStatus(Classes model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
