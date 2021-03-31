using Microsoft.Extensions.DependencyInjection.Extensions;
using Awine.Teach.DocumentService.TencentCos;
using System;
using Awine.Framework.Identity;
using Microsoft.AspNetCore.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class TencentCosServiceCollectionExtensions
    {
        /// <summary>
        /// Using TencentCos Middleware
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> passed to the configuration method.</param>
        /// <param name="setupAction">Middleware configuration options.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTencentCos(this IServiceCollection services, Action<TencentCosOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction != null)
            {
                services.Configure(setupAction); //IOptions<TencentCosOptions>
            }

            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Identity
            services.TryAddSingleton<ICurrentUser, CurrentUser>();

            services.AddHttpClient();

            services.TryAddSingleton<ITencentCosHandler, TencentCosHandler>();

            return services;
        }
    }
}
