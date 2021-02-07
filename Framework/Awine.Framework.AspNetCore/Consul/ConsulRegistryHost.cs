using Awine.Framework.AspNetCore.Consul.Extensions;
using Awine.Framework.AspNetCore.Consul.Registry;
using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.Consul
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public class ConsulRegistryHost : IRegistryHost
    {
        /// <summary>
        /// 版本前辍
        /// </summary>
        private const string VERSION_PREFIX = "version-";

        /// <summary>
        /// 
        /// </summary>
        private readonly ConsulRegistryHostConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        private readonly ConsulClient _consul;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public ConsulRegistryHost(ConsulRegistryHostConfiguration configuration = null)
        {
            _configuration = configuration;

            _consul = new ConsulClient(config =>
            {
                config.Address = new Uri(_configuration.HttpEndpoint);
                if (!string.IsNullOrEmpty(_configuration.Datacenter))
                {
                    config.Datacenter = _configuration.Datacenter;
                }
            });
        }

        /// <summary>
        /// 获取格式化的版本
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        private string GetVersionFromStrings(IEnumerable<string> strings)
        {
            return strings
                ?.FirstOrDefault(x => x.StartsWith(VERSION_PREFIX, StringComparison.Ordinal))
                .TrimStart(VERSION_PREFIX);
        }

        /// <summary>
        /// 异步查找服务实例
        /// </summary>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesAsync()
        {
            return await FindServiceInstancesAsync(nameTagsPredicate: x => true, registryInformationPredicate: x => true);
        }

        /// <summary>
        /// 异步查找服务实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesAsync(string name)
        {
            var queryResult = await _consul.Health.Service(name, tag: "", passingOnly: true);
            var instances = queryResult.Response.Select(serviceEntry => new RegistryInformation
            {
                Name = serviceEntry.Service.Service,
                Address = serviceEntry.Service.Address,
                Port = serviceEntry.Service.Port,
                Version = GetVersionFromStrings(serviceEntry.Service.Tags),
                Tags = serviceEntry.Service.Tags ?? Enumerable.Empty<string>(),
                Id = serviceEntry.Service.ID
            });

            return instances.ToList();
        }


        /// <summary>
        /// 异步获取服务目录
        /// </summary>
        /// <returns></returns>
        private async Task<IDictionary<string, string[]>> GetServicesCatalogAsync()
        {
            var queryResult = await _consul.Catalog.Services(); // local agent datacenter is implied
            var services = queryResult.Response;

            return services;
        }

        /// <summary>
        /// 异步查找服务实例
        /// </summary>
        /// <param name="nameTagsPredicate"></param>
        /// <param name="registryInformationPredicate"></param>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesAsync(Predicate<KeyValuePair<string, string[]>> nameTagsPredicate, Predicate<RegistryInformation> registryInformationPredicate)
        {
            return (await GetServicesCatalogAsync())
                .Where(kvp => nameTagsPredicate(kvp))
                .Select(kvp => kvp.Key)
                .Select(FindServiceInstancesAsync)
                .SelectMany(task => task.Result)
                .Where(x => registryInformationPredicate(x))
                .ToList();
        }

        /// <summary>
        /// 异步查找服务实例
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesAsync(Predicate<KeyValuePair<string, string[]>> predicate)
        {
            return await FindServiceInstancesAsync(nameTagsPredicate: predicate, registryInformationPredicate: x => true);
        }

        /// <summary>
        /// 异步查找服务实例
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesAsync(Predicate<RegistryInformation> predicate)
        {
            return await FindServiceInstancesAsync(nameTagsPredicate: x => true, registryInformationPredicate: predicate);
        }

        /// <summary>
        /// 查找所有服务
        /// </summary>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindAllServicesAsync()
        {
            var queryResult = await _consul.Agent.Services();
            var instances = queryResult.Response.Select(serviceEntry => new RegistryInformation
            {
                Name = serviceEntry.Value.Service,
                Id = serviceEntry.Value.ID,
                Address = serviceEntry.Value.Address,
                Port = serviceEntry.Value.Port,
                Version = GetVersionFromStrings(serviceEntry.Value.Tags),
                Tags = serviceEntry.Value.Tags
            });

            return instances.ToList();
        }

        /// <summary>
        /// GetServiceId
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private string GetServiceId(string serviceName, Uri uri)
        {
            return $"{serviceName}_{uri.Host.Replace(".", "_")}_{uri.Port}";
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="version"></param>
        /// <param name="uri"></param>
        /// <param name="healthCheckUri"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<RegistryInformation> RegisterServiceAsync(string serviceName, string version, Uri uri, Uri healthCheckUri = null, IEnumerable<string> tags = null)
        {
            var serviceId = GetServiceId(serviceName, uri);
            string check = healthCheckUri?.ToString() ?? $"{uri}".TrimEnd('/') + "/health";

            string versionLabel = $"{VERSION_PREFIX}{version}";
            var tagList = (tags ?? Enumerable.Empty<string>()).ToList();
            tagList.Add(versionLabel);

            var registration = new AgentServiceRegistration
            {
                ID = serviceId,
                Name = serviceName,
                Tags = tagList.ToArray(),
                Address = uri.Host,
                Port = uri.Port,
                Check = new AgentServiceCheck { HTTP = check, Interval = TimeSpan.FromSeconds(10) }
            };

            await _consul.Agent.ServiceRegister(registration);

            return new RegistryInformation
            {
                Name = registration.Name,
                Id = registration.ID,
                Address = registration.Address,
                Port = registration.Port,
                Version = version,
                Tags = tagList
            };
        }

        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public async Task<bool> DeregisterServiceAsync(string serviceId)
        {
            var writeResult = await _consul.Agent.ServiceDeregister(serviceId);
            bool isSuccess = writeResult.StatusCode == HttpStatusCode.OK;
            string success = isSuccess ? "succeeded" : "failed";

            return isSuccess;
        }

        /// <summary>
        /// GetCheckId
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private string GetCheckId(string serviceId, Uri uri)
        {
            return $"{serviceId}_{uri.GetPath().Replace("/", "")}";
        }

        /// <summary>
        /// 注册健康检查
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceId"></param>
        /// <param name="checkUri"></param>
        /// <param name="interval"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        public async Task<string> RegisterHealthCheckAsync(string serviceName, string serviceId, Uri checkUri, TimeSpan? interval = null, string notes = null)
        {
            if (checkUri == null)
            {
                throw new ArgumentNullException(nameof(checkUri));
            }

            var checkId = GetCheckId(serviceId, checkUri);
            var checkRegistration = new AgentCheckRegistration
            {
                ID = checkId,
                Name = serviceName,
                Notes = notes,
                ServiceID = serviceId,
                HTTP = checkUri.ToString(),
                Interval = interval
            };
            var writeResult = await _consul.Agent.CheckRegister(checkRegistration);
            bool isSuccess = writeResult.StatusCode == HttpStatusCode.OK;
            string success = isSuccess ? "succeeded" : "failed";

            return checkId;
        }

        /// <summary>
        /// 异步取消健康检查
        /// </summary>
        /// <param name="checkId"></param>
        /// <returns></returns>
        public async Task<bool> DeregisterHealthCheckAsync(string checkId)
        {
            var writeResult = await _consul.Agent.CheckDeregister(checkId);
            bool isSuccess = writeResult.StatusCode == HttpStatusCode.OK;
            string success = isSuccess ? "succeeded" : "failed";

            return isSuccess;
        }

        /// <summary>
        /// 查询选项
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private QueryOptions QueryOptions(ulong index)
        {
            return new QueryOptions
            {
                Datacenter = _configuration.Datacenter,
                Token = _configuration.AclToken ?? "anonymous",
                WaitIndex = index,
                WaitTime = _configuration.LongPollMaxWait
            };
        }

        /// <summary>
        /// 由服务名称和版本查找服务实例
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesWithVersionAsync(string name, string version)
        {
            var instances = await FindServiceInstancesAsync(name);
            return instances.Where(x => x.Version.Equals(version)).ToList();
        }
    }
}
