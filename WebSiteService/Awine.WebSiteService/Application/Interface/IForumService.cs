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
    /// 版块管理
    /// </summary>
    public interface IForumService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="displayAttribute"></param>
        /// <param name="contentAttribute"></param>
        /// <returns></returns>
        Task<IPagedList<ForumViewModel>> GetPageList(int page = 1, int limit = 15, int displayAttribute = 0, int contentAttribute = 0);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="displayAttribute"></param>
        /// <param name="contentAttribute"></param>
        /// <returns></returns>
        Task<IEnumerable<ForumViewModel>> GetAll(int displayAttribute = 0, int contentAttribute = 0);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        Task<IEnumerable<ForumViewModel>> GetAllChilds(string parentId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ForumViewModel> GetModel(string id = "");

        /// <summary>
        /// 树型结构数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<IEnumerable<ForumTreeViewModel>> GetTreeList(string parentId = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(ForumAddViewModel model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(ForumUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
