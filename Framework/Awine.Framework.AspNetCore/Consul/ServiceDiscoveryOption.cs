
namespace Awine.Framework.AspNetCore.Consul
{
    /// <summary>
    /// Please refer to http://michaco.net/blog/ServiceDiscoveryAndHealthChecksInAspNetCoreWithConsul website for details.
    /// </summary>
    public class ConsulServiceDiscoveryOption
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Consul配置信息
        /// </summary>
        public ConsulRegistryHostConfiguration Consul { get; set; }

        /// <summary>
        /// 健康检查
        /// </summary>
        public string HealthCheckTemplate { get; set; }

        /// <summary>
        /// 终结点
        /// </summary>
        public string[] Endpoints { get; set; }
    }
}
