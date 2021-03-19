using Microsoft.Extensions.DependencyInjection;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Text;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.Services;
using Awine.Teach.FoundationService.Domain.Interface;
using Awine.Teach.FoundationService.Infrastructure.Repository;
using Awine.Framework.Dapper.Extensions.Options;
using Awine.Framework.Identity;

namespace Awine.Teach.FoundationService.Application.Extensions
{
    /// <summary>
    /// Foundation ServiceCollection Extensions
    /// </summary>
    public static class FoundationServiceCollectionExtensions
    {
        /// <summary>
        /// Add Foundation MySQLProvider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbProviderOptionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddFoundationMySQLProvider(this IServiceCollection services, Action<MySQLProviderOptions> dbProviderOptionsAction = null)
        {
            var options = new MySQLProviderOptions();
            services.AddSingleton(options);
            dbProviderOptionsAction?.Invoke(options);
            return services;
        }

        /// <summary>
        /// AddFoundationApplication
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFoundationApplication(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            services.Add(ServiceDescriptor.Singleton<IAdministrativeDivisionsRepository, AdministrativeDivisionsRepository>());
            services.Add(ServiceDescriptor.Singleton<IAdministrativeDivisionsService, AdministrativeDivisionsService>());

            services.Add(ServiceDescriptor.Singleton<IIndustryCategoryRepository, IndustryCategoryRepository>());
            services.Add(ServiceDescriptor.Singleton<IIndustryCategoryService, IndustryCategoryService>());

            services.Add(ServiceDescriptor.Singleton<ITenantsRepository, TenantsRepository>());
            services.Add(ServiceDescriptor.Singleton<ITenantsService, TenantsService>());

            services.Add(ServiceDescriptor.Singleton<IDepartmentsRepository, DepartmentsRepository>());
            services.Add(ServiceDescriptor.Singleton<IDepartmentsService, DepartmentsService>());

            services.Add(ServiceDescriptor.Singleton<IModulesRepository, ModulesRepository>());
            services.Add(ServiceDescriptor.Singleton<IModulesService, ModulesService>());

            services.Add(ServiceDescriptor.Singleton<IButtonsRepository, ButtonsRepository>());
            services.Add(ServiceDescriptor.Singleton<IButtonsService, ButtonsService>());

            services.Add(ServiceDescriptor.Singleton<IRolesRepository, RolesRepository>());
            services.Add(ServiceDescriptor.Singleton<IRolesOwnedModulesRepository, RolesOwnedModulesRepository>());
            services.Add(ServiceDescriptor.Singleton<IRolesService, RolesService>());

            services.Add(ServiceDescriptor.Singleton<IRolesClaimsRepository, RolesClaimsRepository>());
            services.Add(ServiceDescriptor.Singleton<IRolesClaimsService, RolesClaimsService>());

            services.Add(ServiceDescriptor.Singleton<IUsersRepository, UsersRepository>());
            services.Add(ServiceDescriptor.Singleton<IUsersService, UsersService>());

            services.Add(ServiceDescriptor.Singleton<IApplicationVersionRepository, ApplicationVersionRepository>());
            services.Add(ServiceDescriptor.Singleton<IApplicationVersionService, ApplicationVersionService>());

            services.Add(ServiceDescriptor.Singleton<IApplicationVersionOwnedModuleRepository, ApplicationVersionOwnedModuleRepository>());
            services.Add(ServiceDescriptor.Singleton<IApplicationVersionOwnedModuleService, ApplicationVersionOwnedModuleService>());

            // Identity
            services.Add(ServiceDescriptor.Singleton<ICurrentUser, CurrentUser>());

            return services;
        }
    }
}
