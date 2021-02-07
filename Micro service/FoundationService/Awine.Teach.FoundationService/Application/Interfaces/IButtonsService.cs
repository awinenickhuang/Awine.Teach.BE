using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// 按钮
    /// </summary>
    public interface IButtonsService
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ButtonsViewModel>> GetAll();

        /// <summary>
        /// 某一模块拥有的按钮列表
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<IEnumerable<ButtonsViewModel>> GetModuleButtons(string moduleId);

        /// <summary>
        /// 某一模块拥有的按钮 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<IPagedList<ButtonsViewModel>> GetPageList(int page = 1, int limit = 15, string moduleId = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(ButtonsAddViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(ButtonsViewModel model);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ButtonsViewModel> GetModel(string id);
    }
}
