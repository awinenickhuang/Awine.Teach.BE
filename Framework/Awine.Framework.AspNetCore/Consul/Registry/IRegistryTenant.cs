using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.AspNetCore.Consul.Registry
{
    public interface IRegistryTenant
    {
        Uri Uri { get; }
    }
}
