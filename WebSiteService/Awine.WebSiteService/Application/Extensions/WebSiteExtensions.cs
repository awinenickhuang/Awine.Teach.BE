using Awine.Framework.Dapper.Extensions.Options;
using Awine.WebSite.Applicaton.Interface;
using Awine.WebSite.Applicaton.Services;
using Awine.WebSite.Domain.Interface;
using Awine.WebSite.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.WebSite.Applicaton.Extensions
{
    public static class WebSiteExtensions
    {
        /// <summary>
        /// Add WebSite MySQLProvider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbProviderOptionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebSiteMySQLProvider(this IServiceCollection services, Action<MySQLProviderOptions> dbProviderOptionsAction = null)
        {
            var options = new MySQLProviderOptions();
            services.AddSingleton(options);
            dbProviderOptionsAction?.Invoke(options);
            return services;
        }

        /// <summary>
        /// Add WebSite Application
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebSiteExtensions(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            //管理员
            services.Add(ServiceDescriptor.Singleton<IManagerRepository, ManagerRepository>());
            services.Add(ServiceDescriptor.Singleton<IManagerService, ManagerService>());

            //横幅图片
            services.Add(ServiceDescriptor.Singleton<IBannerRepository, BannerRepository>());
            services.Add(ServiceDescriptor.Singleton<IBannerService, BannerService>());

            //版块管理
            services.Add(ServiceDescriptor.Singleton<IForumRepository, ForumRepository>());
            services.Add(ServiceDescriptor.Singleton<IForumService, ForumService>());

            //文章管理
            services.Add(ServiceDescriptor.Singleton<IArticleRepository, ArticleRepository>());
            services.Add(ServiceDescriptor.Singleton<IArticleService, ArticleService>());

            //网站设置
            services.Add(ServiceDescriptor.Singleton<ISettingsRepository, SettingsRepository>());
            services.Add(ServiceDescriptor.Singleton<ISettingsService, SettingsService>());

            return services;
        }
    }
}
