using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// 应用版本对应的系统模块
    /// </summary>
    public interface IApplicationVersionOwnedModuleService
    {
        /// <summary>
        /// 设置应用版本包括的模块信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        Task<Result> SaveAppVersionOwnedModules(ApplicationVersionOwnedModuleAddViewModel module);

        /// <summary>
        /// 查询应用版本包括的模块信息
        /// </summary>
        /// <param name="appVersionId"></param>
        /// <returns></returns>
        Task<IEnumerable<ApplicationVersionOwnedModuleViewModel>> GetAppVersionOwnedModules(string appVersionId);
    }
}
