//using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Awine.Framework.Core.Collections
{
    public static class IQueryablePageListExtensions
    {
        /// <summary>
        /// Converts the specified source to <see cref="IPagedList{T}"/> by the specified <paramref name="page"/> and <paramref name="limit"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The source to paging.</param>
        /// <param name="page">The index of the page.</param>
        /// <param name="limit">The size of the page.</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <param name="indexFrom">The start index value.</param>
        /// <returns>An instance of the inherited from <see cref="IPagedList{T}"/> interface.</returns>
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int page, int limit, int indexFrom = 1, CancellationToken cancellationToken = default)
        {
            if (indexFrom > page)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > page: {page}, must indexFrom <= page");
            }

            //var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            //var items = await source.Skip((page - indexFrom) * limit)
            //                        .Take(limit).ToListAsync(cancellationToken).ConfigureAwait(false);

            //var pagedList = new PagedList<T>
            //{
            //    Page = page,
            //    Limit = limit,
            //    IndexFrom = indexFrom,
            //    TotalCount = count,
            //    Items = items,
            //    TotalPages = (int)Math.Ceiling(count / (double)limit)
            //};

            //return pagedList;

            return null;
        }
    }
}
