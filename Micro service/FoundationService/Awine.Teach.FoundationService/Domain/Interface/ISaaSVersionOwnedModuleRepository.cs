using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// SaaS版本包括的系统模块
    /// </summary>
    public interface ISaaSVersionOwnedModuleRepository
    {
        /// <summary>
        /// 设置SaaS版本包括的模块信息
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        Task<bool> SaveSaaSVersionOwnedModules(string saaSVersionId, IList<SaaSVersionOwnedModule> modules);

        /// <summary>
        /// 查询SaaS版本包括的模块信息
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        Task<IEnumerable<SaaSVersionOwnedModule>> GetSaaSVersionOwnedModules(string saaSVersionId);

        /// <summary>
        /// 获取模块集合，以识别模块是否被SaaS版本使用
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<IEnumerable<SaaSVersionOwnedModule>> GetModels(string moduleId);
    }
}
