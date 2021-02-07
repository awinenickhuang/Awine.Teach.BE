using Awine.IdpCenter.DbContexts;
using Awine.IdpCenter.Entities;
using Awine.IdpCenter.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.Repositories
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The DbContext.
        /// </summary>
        protected readonly IUserDbContext Context;

        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly ILogger<UserRepository> Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public UserRepository(IUserDbContext context, ILogger<UserRepository> logger)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Logger = logger;
        }

        /// <summary>
        /// 按ID查询
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public virtual async Task<User> GetBySubjectId(string subjectId)
        {
            IQueryable<User> baseQuery = Context.Users
                .Where(x => x.Id.Equals(subjectId));

            var user = (await baseQuery.ToArrayAsync())
                .SingleOrDefault(x => x.Id.Equals(subjectId));

            if (user == null)
            {
                return null;
            }

            await baseQuery.Include(x => x.Tenant).Select(c => c.Tenant).LoadAsync();
            await baseQuery.Include(x => x.Role).Select(c => c.Role).LoadAsync();

            return user;
        }

        /// <summary>
        /// 按账号查询
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<User> GetByAccount(string account)
        {
            IQueryable<User> baseQuery = Context.Users
                .Where(x => x.Account.Equals(account));

            await baseQuery.Include(x => x.Tenant).Include(x => x.Role).LoadAsync();

            var user = (await baseQuery.ToArrayAsync())
                .SingleOrDefault(x => x.Account.Equals(account));

            if (user == null)
            {
                return null;
            }

            return user;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(User model)
        {
            Context.Users.Update(model);
            return await Context.SaveChangesAsync();
        }
    }
}
