using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.AspNetCore.Consul.Registry
{
    /// <summary>
    /// 
    /// </summary>
    public class RegistryInformation
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public Uri ToUri(string scheme = "http", string path = "/")
        {
            var builder = new UriBuilder(scheme, Address, Port, path);
            return builder.Uri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Address}:{Port}";
        }
    }
}
