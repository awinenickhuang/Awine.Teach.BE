using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// SaaS版本包括的系统模块
    /// </summary>
    public interface ISaaSVersionOwnedModuleService
    {
        /// <summary>
        /// 设置SaaS版本包括的模块信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        Task<Result> SaveSaaSVersionOwnedModules(SaaSVersionOwnedModuleAddViewModel module);

        /// <summary>
        /// 查询SaaS版本包括的模块信息
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        Task<IEnumerable<SaaSVersionOwnedModuleViewModel>> GetSaaSVersionOwnedModules(string saaSVersionId);
    }
}
