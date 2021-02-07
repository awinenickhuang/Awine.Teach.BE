using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.WebSite.Domain.Interface
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public interface IArticleRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="forumId"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="contentSource"></param>
        /// <returns></returns>
        Task<IPagedList<Article>> GetPageList(int page = 1, int limit = 15, string forumId = "", string title = "", string author = "", string contentSource = "");

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="forumId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        Task<IEnumerable<Article>> GetAll(string forumId, int number = 0);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Article> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        Task<Article> GetModelForumAccording(string forumId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(Article model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(Article model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
