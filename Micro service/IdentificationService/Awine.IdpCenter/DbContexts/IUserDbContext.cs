using Awine.IdpCenter.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.DbContexts
{
    /// <summary>
    /// Abstraction for the users data context.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUserDbContext : IDisposable
    {
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the tenants.
        /// </summary>
        /// <value>
        /// The tenants.
        /// </value>
        DbSet<Tenant> Tenants { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the departments.
        /// </summary>
        /// <value>
        /// The departments.
        /// </value>
        DbSet<Department> Departments { get; set; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
