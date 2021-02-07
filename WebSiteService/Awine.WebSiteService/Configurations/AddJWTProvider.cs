using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.WebSiteService.Configurations
{
    public static class AddJWTProvider
    {
        public static IServiceCollection AddJWTOptions(this IServiceCollection services, Action<JwtSetting> dbProviderOptionsAction = null)
        {
            var options = new JwtSetting();
            services.AddSingleton(options);
            dbProviderOptionsAction?.Invoke(options);
            return services;
        }
    }
}
