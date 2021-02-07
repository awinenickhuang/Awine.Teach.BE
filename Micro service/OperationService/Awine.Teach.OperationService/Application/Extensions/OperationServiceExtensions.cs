using Awine.Framework.Dapper.Extensions.Options;
using Awine.Framework.Identity;
using Awine.Teach.OperationService.Application.Interfaces;
using Awine.Teach.OperationService.Application.Services;
using Awine.Teach.OperationService.Application.SubscriberService;
using Awine.Teach.OperationService.Domain.Interface;
using Awine.Teach.OperationService.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Awine.Teach.OperationService.Application.Extensions
{
    /// <summary>
    /// Operation Service ServiceCollection Extensions
    /// </summary>
    public static class OperationServiceExtensions
    {
        /// <summary>
        /// Add Operation Service MySQLProvider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbProviderOptionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddOperationServiceMySQLProvider(this IServiceCollection services, Action<MySQLProviderOptions> dbProviderOptionsAction = null)
        {
            var options = new MySQLProviderOptions();
            services.AddSingleton(options);
            dbProviderOptionsAction?.Invoke(options);
            return services;
        }

        /// <summary>
        /// Add Operation Service Application
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOperationServiceApplication(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddOptions();

            //用户登录日志 -> 消费并记录
            services.AddTransient<ITenantLoggingSubscriberService, TenantLoggingSubscriberService>();

            //平台公告
            services.Add(ServiceDescriptor.Singleton<IAnnouncementRepository, AnnouncementRepository>());
            services.Add(ServiceDescriptor.Singleton<IAnnouncementService, AnnouncementService>());

            // Identity
            services.Add(ServiceDescriptor.Singleton<ICurrentUser, CurrentUser>());

            //行业资讯
            services.Add(ServiceDescriptor.Singleton<INewsRepository, NewsRepository>());
            services.Add(ServiceDescriptor.Singleton<INewsService, NewsService>());

            //反馈信息
            services.Add(ServiceDescriptor.Singleton<IFeedbackRepository, FeedbackRepository>());
            services.Add(ServiceDescriptor.Singleton<IFeedbackService, FeedbackService>());

            //租户登录信息
            services.Add(ServiceDescriptor.Singleton<ITenantLoggingRepository, TenantLoggingRepository>());
            services.Add(ServiceDescriptor.Singleton<ITenantLoggingService, TenantLoggingService>());

            return services;
        }
    }
}
