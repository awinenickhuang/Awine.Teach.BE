using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// 机构设置
    /// </summary>
    public interface ITenantSettingsService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IPagedList<TenantSettingsViewModel>> GetPageList(int page = 1, int limit = 15);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TenantSettingsViewModel>> GetAll();

        /// <summary>
        /// 取当前登录机构的设置信息
        /// </summary>
        /// <returns></returns>
        Task<TenantSettingsViewModel> GetTenantSettings();

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TenantSettingsViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(TenantSettingsAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(TenantSettingsUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
