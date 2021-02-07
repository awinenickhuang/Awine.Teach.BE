using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// 按钮
    /// </summary>
    public interface IButtonsRepository
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Buttons>> GetAll();

        /// <summary>
        /// 某一模块拥有的按钮列表
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<IEnumerable<Buttons>> GetModuleButtons(string moduleId);

        /// <summary>
        /// 某一模块拥有的按钮 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<IPagedList<Buttons>> GetPageList(int page = 1, int limit = 15, string moduleId = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(Buttons model);

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
        Task<int> Update(Buttons model);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Buttons> GetModel(string id);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Buttons> GetModel(Buttons model);
    }
}
