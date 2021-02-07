using Awine.IdpCenter.Entities;
using Awine.IdpCenter.Extensions;
using Awine.IdpCenter.Options;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.DbContexts
{
    /// <summary>
    /// DbContext for the IdentityServer operational data.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// <seealso cref="IdentityServer4.EntityFramework.Interfaces.IUserDbContext" />
    public class UserDbContext : UserDbContext<UserDbContext>, IUserDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="userOptions">The store options.</param>
        /// <exception cref="ArgumentNullException">storeOptions</exception>
        public UserDbContext(DbContextOptions<UserDbContext> options, UserStoreOptions userOptions)
            : base(options, userOptions)
        {
        }
    }

    /// <summary>
    /// DbContext for the IdentityServer operational data.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// <seealso cref="IdentityServer4.EntityFramework.Interfaces.IUserDbContext" />
    public class UserDbContext<TContext> : DbContext, IUserDbContext
        where TContext : DbContext, IUserDbContext
    {
        private readonly UserStoreOptions userOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="userOptions">The store options.</param>
        /// <exception cref="ArgumentNullException">storeOptions</exception>
        public UserDbContext(DbContextOptions options, UserStoreOptions userOptions)
            : base(options)
        {
            if (userOptions == null) throw new ArgumentNullException(nameof(userOptions));
            this.userOptions = userOptions;
        }

        /// <summary>
        /// Gets or sets the Users.
        /// </summary>
        /// <value>
        /// The Users.
        /// </value>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the Tenants.
        /// </summary>
        /// <value>
        /// The Tenants.
        /// </value>
        public DbSet<Tenant> Tenants { get; set; }

        /// <summary>
        /// Gets or sets the Tenants.
        /// </summary>
        /// <value>
        /// The Tenants.
        /// </value>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the Tenants.
        /// </summary>
        /// <value>
        /// The Tenants.
        /// </value>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        public virtual Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUserContext(userOptions);

            base.OnModelCreating(modelBuilder);
        }
    }
}
