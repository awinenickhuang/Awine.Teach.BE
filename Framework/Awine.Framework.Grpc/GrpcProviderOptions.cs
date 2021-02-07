using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.Grpc
{
    /// <summary>
    /// Grpc Provider Options
    /// </summary>
    public class GrpcProviderOptions
    {
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; }
    }
}
