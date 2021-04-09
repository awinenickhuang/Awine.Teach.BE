using Awine.IdpCenter.Entities;
using Awine.IdpCenter.Options;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Awine.IdpCenter.Extensions
{
    /// <summary>
    /// Extension methods to define the database schema for the user data stores.
    /// </summary>
    public static class UserModelBuilderExtensions
    {
        private static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, TableConfiguration configuration)
            where TEntity : class
        {
            return string.IsNullOrWhiteSpace(configuration.Schema) ? entityTypeBuilder.ToTable(configuration.Name) : entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
        }

        /// <summary>
        /// Configures the user context.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <param name="storeOptions">The store options.</param>
        public static void ConfigureUserContext(this ModelBuilder modelBuilder, UserStoreOptions userOptions)
        {
            if (!string.IsNullOrWhiteSpace(userOptions.DefaultSchema)) modelBuilder.HasDefaultSchema(userOptions.DefaultSchema);

            modelBuilder.Entity<User>(user =>
            {
                user.ToTable(userOptions.Users);

                //user.Property(x => x.Id).HasMaxLength(36).ValueGeneratedNever();
                //user.Property(x => x.IsActive).IsRequired();
                //user.Property(x => x.AccessFailedCount).IsRequired();
                //user.Property(x => x.ConcurrencyStamp).HasMaxLength(36).IsRequired();
                //user.Property(x => x.Email).HasMaxLength(64);
                //user.Property(x => x.EmailConfirmed).IsRequired();
                //user.Property(x => x.LockoutEnabled).IsRequired();
                //user.Property(x => x.LockoutEnd);
                //user.Property(x => x.NormalizedEmail).HasMaxLength(64);
                //user.Property(x => x.NormalizedUserName).HasMaxLength(32);
                //user.Property(x => x.UserName).HasMaxLength(32).IsRequired();
                //user.Property(x => x.Account).HasMaxLength(32).IsRequired();
                //user.Property(x => x.PasswordHash).HasMaxLength(128).IsRequired();
                //user.Property(x => x.PhoneNumber).HasMaxLength(32).IsRequired();
                //user.Property(x => x.PhoneNumberConfirmed).IsRequired();
                //user.Property(x => x.SecurityStamp).HasMaxLength(64).IsRequired();
                //user.Property(x => x.TwoFactorEnabled).IsRequired();
                //user.Property(x => x.Gender).IsRequired();
                //user.Property(x => x.TenantId).HasMaxLength(36).IsRequired();
                //user.Property(x => x.RoleId).HasMaxLength(36).IsRequired();
                //user.Property(x => x.DepartmentId).HasMaxLength(36).IsRequired();
                //user.Property(x => x.IsDeleted).IsRequired();
                //user.Property(x => x.CreateTime).IsRequired();

                user.HasKey(x => x.Id);

                //user.HasIndex(x => new { x.Id, x.TenantId, x.RoleId });
            });

            modelBuilder.Entity<Tenant>(tenant =>
            {
                tenant.ToTable(userOptions.Tenants);

                //tenant.Property(x => x.Id).HasMaxLength(36).IsRequired();
                //tenant.Property(x => x.ParentId).HasMaxLength(36).IsRequired();
                //tenant.Property(x => x.Name).HasMaxLength(64);
                //tenant.Property(x => x.Contacts).HasMaxLength(32);
                //tenant.Property(x => x.ContactsPhone).HasMaxLength(32).IsRequired();
                //tenant.Property(x => x.ClassiFication).IsRequired();
                //tenant.Property(x => x.Status).IsRequired();
                //tenant.Property(x => x.ProvinceId).IsRequired();
                //tenant.Property(x => x.ProvinceName).IsRequired();
                //tenant.Property(x => x.CityId).IsRequired();
                //tenant.Property(x => x.CityName).IsRequired();
                //tenant.Property(x => x.DistrictId).IsRequired();
                //tenant.Property(x => x.DistrictName).IsRequired();
                //tenant.Property(x => x.Address).IsRequired();
                //tenant.Property(x => x.VIPExpirationTime).IsRequired();
                //tenant.Property(x => x.IndustryId).IsRequired();
                //tenant.Property(x => x.IndustryName).IsRequired();
                //tenant.Property(x => x.CreateTime).IsRequired();

                //tenant.HasIndex(x => new { x.Id });

                tenant.HasKey(x => new { x.Id });
            });

            modelBuilder.Entity<Role>(role =>
            {
                role.ToTable(userOptions.Roles);

                //role.Property(x => x.Id).HasMaxLength(36).IsRequired();
                //role.Property(x => x.Name).HasMaxLength(64);
                //role.Property(x => x.ConcurrencyStamp).HasMaxLength(32);
                //role.Property(x => x.NormalizedName).HasMaxLength(32).IsRequired();
                //role.Property(x => x.TenantId).IsRequired();
                //role.Property(x => x.CreateTime).IsRequired();

                //role.HasIndex(x => new { x.Id, x.TenantId });

                role.HasKey(x => new { x.Id });
            });
        }
    }
}
