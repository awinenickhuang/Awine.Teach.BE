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
    /// 角色
    /// </summary>
    public interface IRolesService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IPagedList<RolesViewModel>> GetPageList(int pageIndex = 1, int pageSize = 15, string tenantId = "");

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IEnumerable<RolesViewModel>> GetAll(string tenantId = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(RolesAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(RolesUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RolesViewModel> GetModel(string id);

        /// <summary>
        /// 保存 -> 角色拥有的模块信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> SaveRoleOwnedModules(RolesOwnedModulesAddViewModel model);

        /// <summary>
        /// 查询 -> 角色拥有的模块信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IList<RolesOwnedModulesViewModel>> GetRoleOwnedModules(string roleId);
    }
}
