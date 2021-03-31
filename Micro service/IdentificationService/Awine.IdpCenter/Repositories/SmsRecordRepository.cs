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
    /// <summary>
    /// 手机验证码
    /// </summary>
    public class SmsRecordRepository : ISmsRecordRepository
    {
        /// <summary>
        /// The DbContext.
        /// </summary>
        protected readonly IUserDbContext Context;

        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly ILogger<SmsRecordRepository> Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public SmsRecordRepository(IUserDbContext context, ILogger<SmsRecordRepository> logger)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Logger = logger;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(SmsRecord model)
        {
            await Context.SmsRecord.AddAsync(model);
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// 按手机号查询验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<SmsRecord> GetAsync(string phone)
        {
            IQueryable<SmsRecord> baseQuery = Context.SmsRecord.Where(x => x.Receiver.Equals(phone));

            return (await baseQuery.ToArrayAsync()).SingleOrDefault(x => x.Receiver.Equals(phone));
        }
    }
}
