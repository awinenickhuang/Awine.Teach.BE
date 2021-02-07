using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.Options
{
    /// <summary>
    /// Options for configuring the user context.
    /// </summary>
    public class UserStoreOptions
    {
        /// <summary>
        /// Callback to configure the EF DbContext.
        /// </summary>
        /// <value>
        /// The configure database context.
        /// </value>
        public Action<DbContextOptionsBuilder> ConfigureDbContext { get; set; }

        /// <summary>
        /// Callback in DI resolve the EF DbContextOptions. If set, UserDbContext will not be used.
        /// </summary>
        /// <value>
        /// The configure database context.
        /// </value>
        public Action<IServiceProvider, DbContextOptionsBuilder> ResolveDbContextOptions { get; set; }

        /// <summary>
        /// Gets or sets the default schema.
        /// </summary>
        /// <value>
        /// The default schema.
        /// </value>
        public string DefaultSchema { get; set; } = null;

        /// <summary>
        /// Gets or sets the users table configuration.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public TableConfiguration Users { get; set; } = new TableConfiguration("Users");

        /// <summary>
        /// Gets or sets the tenants table configuration.
        /// </summary>
        /// <value>
        /// The tenants.
        /// </value>
        public TableConfiguration Tenants { get; set; } = new TableConfiguration("Tenants");

        /// <summary>
        /// Gets or sets the roles table configuration.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public TableConfiguration Roles { get; set; } = new TableConfiguration("Roles");

        /// <summary>
        /// Gets or sets the departments table configuration.
        /// </summary>
        /// <value>
        /// The departments.
        /// </value>
        public TableConfiguration Departments { get; set; } = new TableConfiguration("Departments");
    }
}
