using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 教室管理
    /// </summary>
    public interface IClassRoomRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IPagedList<ClassRoom>> GetPageList(int page = 1, int limit = 15, string tenantId = "");

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IEnumerable<ClassRoom>> GetAll(string tenantId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ClassRoom> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ClassRoom> GetModel(ClassRoom model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(ClassRoom model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(ClassRoom model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
