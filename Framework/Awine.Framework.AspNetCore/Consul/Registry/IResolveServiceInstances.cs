using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.Consul.Registry
{
    /// <summary>
    /// 服务实例解析接口
    /// </summary>
    public interface IResolveServiceInstances
    {
        /// <summary>
        /// 异步查找服务实例
        /// </summary>
        /// <returns></returns>
        Task<IList<RegistryInformation>> FindServiceInstancesAsync();

        /// <summary>
        ///异步查找服务实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IList<RegistryInformation>> FindServiceInstancesAsync(string name);

        /// <summary>
        /// 异步查找服务实例
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        Task<IList<RegistryInformation>> FindServiceInstancesWithVersionAsync(string name, string version);

        /// <summary>
        /// 异步查找服务实例
        /// </summary>
        /// <param name="nameTagsPredicate"></param>
        /// <param name="registryInformationPredicate"></param>
        /// <returns></returns>
        Task<IList<RegistryInformation>> FindServiceInstancesAsync(Predicate<KeyValuePair<string, string[]>> nameTagsPredicate,
            Predicate<RegistryInformation> registryInformationPredicate);

        /// <summary>
        /// 异步查找服务实例
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<RegistryInformation>> FindServiceInstancesAsync(Predicate<KeyValuePair<string, string[]>> predicate);

        /// <summary>
        /// 异步查找服务实例
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<RegistryInformation>> FindServiceInstancesAsync(Predicate<RegistryInformation> predicate);

        /// <summary>
        /// 异步查找所有服务
        /// </summary>
        /// <returns></returns>
        Task<IList<RegistryInformation>> FindAllServicesAsync();
    }
}
