using Awine.Framework.AspNetCore.Consul.Registry;
using Consul;
using DnsClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Net;

namespace Awine.Framework.AspNetCore.Consul
{
    /// <summary>
    /// ServiceCollectionExtensions
    /// </summary>
    public static class ConsulServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Consul功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAwineConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<ConsulServiceDiscoveryOption>(configuration.GetSection("ServiceDiscovery"));
            services.RegisterConsulClient();
            //services.RegisterDnsLookup();
            ConsulServiceDiscoveryOption serviceDiscoveryOption = new ConsulServiceDiscoveryOption();
            configuration.GetSection("ServiceDiscovery").Bind(serviceDiscoveryOption);
            services.AddAwineHost(() => new ConsulRegistryHost(serviceDiscoveryOption.Consul));
            return services;
        }

        /// <summary>
        /// 添加Consul功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="consulRegistryConfiguration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAwineConsul(this IServiceCollection services, ConsulRegistryHostConfiguration consulRegistryConfiguration)
        {
            services.AddAwineHost(() => new ConsulRegistryHost(consulRegistryConfiguration));
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="registryHostFactory"></param>
        /// <returns></returns>
        public static IServiceCollection AddAwineHost(this IServiceCollection services, Func<IRegistryHost> registryHostFactory)
        {
            var registryHost = registryHostFactory();
            var serviceRegistry = new ServiceRegistry(registryHost);
            services.AddSingleton(serviceRegistry);
            //services.AddTransient<IStartupFilter, NanoStartupFilter>();
            return services;
        }

        #region Private Methods

        /// <summary>
        /// RegisterDnsLookup
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection RegisterDnsLookup(this IServiceCollection services)
        {
            //implement the dns lookup and register to service container
            services.TryAddSingleton<IDnsQuery>(p =>
            {
                var serviceConfiguration = p.GetRequiredService<IOptions<ConsulServiceDiscoveryOption>>().Value;
                //Default
                var client = new LookupClient(IPAddress.Parse("127.0.0.1"), 8600);
                if (serviceConfiguration.Consul.DnsEndpoint != null)
                {
                    client = new LookupClient(serviceConfiguration.Consul.DnsEndpoint.ToIPEndPoint());
                }
                client.EnableAuditTrail = false;
                client.UseCache = true;
                client.MinimumCacheTimeout = TimeSpan.FromSeconds(1);
                return client;
            });
            return services;
        }

        /// <summary>
        /// Consul client config -> implement the consulclient and add to service container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection RegisterConsulClient(this IServiceCollection services)
        {
            services.TryAddSingleton<IConsulClient>(p => new ConsulClient(config =>
            {
                config.Address = new Uri("http://127.0.0.1:8500");//默认设置的地址
                var serviceConfiguration = p.GetRequiredService<IOptions<ConsulServiceDiscoveryOption>>().Value;
                if (!string.IsNullOrEmpty(serviceConfiguration.Consul.HttpEndpoint))
                {
                    config.Address = new Uri(serviceConfiguration.Consul.HttpEndpoint);//读取配置的地址
                }
            }));
            return services;
        }

        #endregion
    }
}
