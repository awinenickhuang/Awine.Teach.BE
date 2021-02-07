using Awine.IdpCenter.DbContexts;
using Awine.IdpCenter.IRepositories;
using Awine.IdpCenter.Options;
using Awine.IdpCenter.Repositories;
using Awine.IdpCenter.Services;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IIdentityServerBuilder AddUserStore(this IIdentityServerBuilder builder,
            Action<UserStoreOptions> userOptionsAction = null)
        {
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            return builder.AddUserStore<UserDbContext>(userOptionsAction);
        }

        public static IIdentityServerBuilder AddUserStore<TContext>(this IIdentityServerBuilder builder,
            Action<UserStoreOptions> userOptionsAction = null) where TContext : DbContext, IUserDbContext
        {
            var options = new UserStoreOptions();
            builder.Services.AddSingleton(options);
            userOptionsAction?.Invoke(options);

            //builder.Services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

            //builder.AddProfileService<ProfileService>();

            if (options.ResolveDbContextOptions != null)
            {
                builder.Services.AddDbContext<TContext>(options.ResolveDbContextOptions);
            }
            else
            {
                builder.Services.AddDbContext<TContext>(dbCtxBuilder =>
                {
                    options.ConfigureDbContext?.Invoke(dbCtxBuilder);
                });
            }
            builder.Services.AddScoped<IUserDbContext, TContext>();

            return builder;
        }
    }
}
