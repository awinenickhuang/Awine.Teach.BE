using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.AspNetCore.Consul.Registry
{
    public class WebApiRegistryTenant : IRegistryTenant
    {
        public Uri Uri { get; }

        public WebApiRegistryTenant(Uri uri)
        {
            Uri = uri;
        }
    }
}
