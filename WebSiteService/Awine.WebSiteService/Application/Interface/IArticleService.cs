using Awine.Framework.Core.Collections;
using Awine.WebSite.Applicaton.ServiceResult;
using Awine.WebSite.Applicaton.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.WebSite.Applicaton.Interface
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public interface IArticleService
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
        Task<IPagedList<ArticleViewModel>> GetPageList(int page = 1, int limit = 15, string forumId = "", string title = "", string author = "", string contentSource = "");

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="forumId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        Task<IEnumerable<ArticleViewModel>> GetAll(string forumId = "", int number = 0);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ArticleViewModel> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        Task<ArticleViewModel> GetModelForumAccording(string forumId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(ArticleAddViewModel model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(ArticleUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
