using System;
using System.Net;

namespace Awine.Framework.AspNetCore.Consul
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsulRegistryHostConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public string HttpEndpoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DnsEndpoint DnsEndpoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Datacenter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AclToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan? LongPollMaxWait { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan? RetryDelay { get; set; } = Defaults.ErrorRetryInterval;
    }

    /// <summary>
    /// 
    /// </summary>
    public class DnsEndpoint
    {
        public string Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint ToIPEndPoint()
        {
            return new IPEndPoint(IPAddress.Parse(Address), Port);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// 
        /// </summary>
        public static TimeSpan ErrorRetryInterval => TimeSpan.FromSeconds(15);

        /// <summary>
        /// 
        /// </summary>
        public static TimeSpan UpdateMaxInterval => TimeSpan.FromSeconds(15);
    }
}
