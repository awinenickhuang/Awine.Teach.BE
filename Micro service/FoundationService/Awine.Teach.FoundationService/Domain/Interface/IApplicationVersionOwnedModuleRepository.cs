using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// 应用版本对应的系统模块
    /// </summary>
    public interface IApplicationVersionOwnedModuleRepository
    {
        /// <summary>
        /// 设置应用版本包括的模块信息
        /// </summary>
        /// <param name="appVersionId"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        Task<bool> SaveAppVersionOwnedModules(string appVersionId, IList<ApplicationVersionOwnedModule> modules);

        /// <summary>
        /// 查询应用版本包括的模块信息
        /// </summary>
        /// <param name="appVersionId"></param>
        /// <returns></returns>
        Task<IEnumerable<ApplicationVersionOwnedModule>> GetAppVersionOwnedModules(string appVersionId);

        /// <summary>
        /// 获取模块集合，以识别模块是否被应用版本使用
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<IEnumerable<ApplicationVersionOwnedModule>> GetModels(string moduleId);
    }
}
