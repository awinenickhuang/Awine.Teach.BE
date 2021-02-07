using Awine.Framework.Dapper.Extensions.Options;
using Awine.Framework.Identity;
using Awine.Teach.FinancialService.Application.Interfaces;
using Awine.Teach.FinancialService.Application.Services;
using Awine.Teach.FinancialService.Domain.Interface;
using Awine.Teach.FinancialService.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FinancialService.Application.Extensions
{
    /// <summary>
    /// Financial Service Extensions
    /// </summary>
    public static class FinancialServiceExtensions
    {
        /// <summary>
        /// Add FinancialService MySQL Provider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbProviderOptionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddFinancialServiceMySQLProvider(this IServiceCollection services, Action<MySQLProviderOptions> dbProviderOptionsAction = null)
        {
            var options = new MySQLProviderOptions();
            services.AddSingleton(options);
            dbProviderOptionsAction?.Invoke(options);
            return services;
        }

        /// <summary>
        /// Add Financial Service Application
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFinancialServiceApplication(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddOptions();

            //财务收支项目
            services.Add(ServiceDescriptor.Singleton<IFinancialItemRepository, FinancialItemRepository>());
            services.Add(ServiceDescriptor.Singleton<IFinancialItemService, FinancialItemService>());

            //日常支出
            services.Add(ServiceDescriptor.Singleton<IDailySpendingRepository, DailySpendingRepository>());
            services.Add(ServiceDescriptor.Singleton<IDailySpendingService, DailySpendingService>());

            // Identity
            services.Add(ServiceDescriptor.Singleton<ICurrentUser, CurrentUser>());

            return services;
        }
    }
}
