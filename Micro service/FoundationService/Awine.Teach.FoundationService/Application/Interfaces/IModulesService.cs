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
    /// 系统模块
    /// </summary>
    public interface IModulesService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ModulesViewModel>> GetAll();

        /// <summary>
        /// 带选中状态的列表 -> 设置角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IEnumerable<ModulesWithCheckedStatusViewModel>> GetAllWithChedkedStatus(string roleId);

        /// <summary>
        /// 带选中状态的列表 -> 设置SaaS版本包括的模块
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        Task<IEnumerable<ModulesWithCheckedStatusViewModel>> GetAllWithChedkedStatusForAppVersion(string saaSVersionId);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IPagedList<ModulesViewModel>> GetPageList(int page = 1, int limit = 15);

        /// <summary>
        /// 获取一个模块的子模块列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<IEnumerable<ModulesViewModel>> GetAllChilds(string parentId);

        /// <summary>
        /// 树列表
        /// </summary>
        /// <param name="moduleParentId"></param>
        /// <returns></returns>
        Task<IEnumerable<ModulesTreeViewModel>> GetTreeList(string moduleParentId = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(ModulesAddViewModel model);

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
        Task<Result> Update(ModulesUpdateViewModel model);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ModulesViewModel> GetModel(string id);

        /// <summary>
        /// 获取当前用户角色拥有的模块信息 -> 生成系统菜单
        /// </summary>
        /// <returns></returns>
        Task<Result<IEnumerable<ModulesTreeViewModel>>> GetRoleOwnedModules();
    }
}
