using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.Consul.Registry
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceRegistry : IManageServiceInstances, IManageHealthChecks, IResolveServiceInstances
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRegistryHost _registryHost;

        /// <summary>
        /// 
        /// </summary>
        private IResolveServiceInstances _serviceInstancesResolver;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registryHost"></param>
        public ServiceRegistry(IRegistryHost registryHost)
        {
            _registryHost = registryHost;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="version"></param>
        /// <param name="uri"></param>
        /// <param name="healthCheckUri"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<RegistryInformation> RegisterServiceAsync(string serviceName, string version, Uri uri, Uri healthCheckUri = null, IEnumerable<string> tags = null)
        {
            var registryInformation = await _registryHost.RegisterServiceAsync(serviceName, version, uri, healthCheckUri, tags);

            return registryInformation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public async Task<bool> DeregisterServiceAsync(string serviceId)
        {
            return await _registryHost.DeregisterServiceAsync(serviceId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceId"></param>
        /// <param name="checkUri"></param>
        /// <param name="interval"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        public async Task<string> RegisterHealthCheckAsync(string serviceName, string serviceId, Uri checkUri, TimeSpan? interval = null, string notes = null)
        {
            return await _registryHost.RegisterHealthCheckAsync(serviceName, serviceId, checkUri, interval, notes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkId"></param>
        /// <returns></returns>
        public async Task<bool> DeregisterHealthCheckAsync(string checkId)
        {
            return await _registryHost.DeregisterHealthCheckAsync(checkId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesAsync()
        {
            return _serviceInstancesResolver == null
                ? await _registryHost.FindServiceInstancesAsync()
                : await _serviceInstancesResolver.FindServiceInstancesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesAsync(string name)
        {
            return _serviceInstancesResolver == null
                ? await _registryHost.FindServiceInstancesAsync(name)
                : await _serviceInstancesResolver.FindServiceInstancesAsync(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesWithVersionAsync(string name, string version)
        {
            return _serviceInstancesResolver == null
                ? await _registryHost.FindServiceInstancesWithVersionAsync(name, version)
                : await _serviceInstancesResolver.FindServiceInstancesWithVersionAsync(name, version);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameTagsPredicate"></param>
        /// <param name="registryInformationPredicate"></param>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesAsync(Predicate<KeyValuePair<string, string[]>> nameTagsPredicate,
            Predicate<RegistryInformation> registryInformationPredicate)
        {
            return _serviceInstancesResolver == null
                ? await _registryHost.FindServiceInstancesAsync(nameTagsPredicate, registryInformationPredicate)
                : await _serviceInstancesResolver.FindServiceInstancesAsync(nameTagsPredicate, registryInformationPredicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesAsync(Predicate<KeyValuePair<string, string[]>> predicate)
        {
            return _serviceInstancesResolver == null
                ? await _registryHost.FindServiceInstancesAsync(predicate)
                : await _serviceInstancesResolver.FindServiceInstancesAsync(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindServiceInstancesAsync(Predicate<RegistryInformation> predicate)
        {
            return _serviceInstancesResolver == null
                ? await _registryHost.FindServiceInstancesAsync(predicate)
                : await _serviceInstancesResolver.FindServiceInstancesAsync(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<RegistryInformation>> FindAllServicesAsync()
        {
            return _serviceInstancesResolver == null
                ? await _registryHost.FindAllServicesAsync()
                : await _serviceInstancesResolver.FindAllServicesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registryTenant"></param>
        /// <param name="serviceName"></param>
        /// <param name="version"></param>
        /// <param name="healthCheckUri"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<RegistryInformation> AddTenantAsync(IRegistryTenant registryTenant, string serviceName, string version, Uri healthCheckUri = null, IEnumerable<string> tags = null)
        {
            var uri = registryTenant.Uri;
            return await RegisterServiceAsync(serviceName, version, uri, healthCheckUri, tags);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceId"></param>
        /// <param name="checkUri"></param>
        /// <param name="interval"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        public async Task<string> AddHealthCheckAsync(string serviceName, string serviceId, Uri checkUri, TimeSpan? interval = null, string notes = null)
        {
            return await RegisterHealthCheckAsync(serviceName, serviceId, checkUri, interval, notes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceInstancesResolver"></param>
        public void ResolveServiceInstancesWith<T>(T serviceInstancesResolver)
            where T : IResolveServiceInstances
        {
            _serviceInstancesResolver = serviceInstancesResolver;
        }
    }
}
