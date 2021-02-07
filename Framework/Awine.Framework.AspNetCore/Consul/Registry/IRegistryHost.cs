using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.AspNetCore.Consul.Registry
{
    /// <summary>
    /// 服务注册接口
    /// </summary>
    public interface IRegistryHost : IManageServiceInstances, IManageHealthChecks, IResolveServiceInstances
    {
        //
    }
}
