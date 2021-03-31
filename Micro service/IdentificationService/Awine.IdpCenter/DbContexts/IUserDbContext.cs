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
        /// Gets or sets the Users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the Tenants.
        /// </summary>
        /// <value>
        /// The tenants.
        /// </value>
        DbSet<Tenant> Tenants { get; set; }

        /// <summary>
        /// Gets or sets the Roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the Departments.
        /// </summary>
        /// <value>
        /// The departments.
        /// </value>
        DbSet<Department> Departments { get; set; }

        /// <summary>
        /// Gets or sets the SmsRecord.
        /// </summary>
        DbSet<SmsRecord> SmsRecord { get; set; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
