using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// 系统模块
    /// </summary>
    public interface IModulesRepository
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Modules>> GetAll();

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IPagedList<Modules>> GetPageList(int page = 1, int limit = 15);

        /// <summary>
        /// 获取一个模块的子模块列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<IEnumerable<Modules>> GetAllChilds(string parentId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(Modules model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(Modules model);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Modules> GetModel(string id);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Modules> GetModel(Modules model);
    }
}
