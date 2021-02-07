using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.WebSite.Domain.Interface
{
    /// <summary>
    /// 版块管理
    /// </summary>
    public interface IForumRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="displayAttribute"></param>
        /// <param name="contentAttribute"></param>
        /// <returns></returns>
        Task<IPagedList<Forum>> GetPageList(int page = 1, int limit = 15, int displayAttribute = 0, int contentAttribute = 0);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="displayAttribute"></param>
        /// <param name="contentAttribute"></param>
        /// <returns></returns>
        Task<IEnumerable<Forum>> GetAll(int displayAttribute = 0, int contentAttribute = 0);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<IEnumerable<Forum>> GetAllChilds(string parentId);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Forum> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(Forum model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(Forum model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
