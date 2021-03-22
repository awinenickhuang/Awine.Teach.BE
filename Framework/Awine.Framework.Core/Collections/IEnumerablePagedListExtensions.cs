using System;
using System.Collections.Generic;

namespace Awine.Framework.Core.Collections
{
    /// <summary>
    /// Provides some extension methods for <see cref="IEnumerable{T}"/> to provide paging capability.
    /// </summary>
    public static class IEnumerablePagedListExtensions
    {
        /// <summary>
        /// Converts the specified source to <see cref="IPagedList{T}"/> by the specified <paramref name="page"/> and <paramref name="limit"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The source to paging.</param>
        /// <param name="page">The index of the page.</param>
        /// <param name="limit">The size of the page.</param>
        /// <param name="indexFrom">The start index value.</param>
        /// <returns>An instance of the inherited from <see cref="IPagedList{T}"/> interface.</returns>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int page, int limit, int indexFrom = 1) => new PagedList<T>(source, page, limit, indexFrom);

        /// <summary>
        /// Converts the specified source to <see cref="IPagedList{T}"/> by the specified <paramref name="converter"/>, <paramref name="page"/> and <paramref name="limit"/>
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TResult">The type of the result</typeparam>
        /// <param name="source">The source to convert.</param>
        /// <param name="converter">The converter to change the <typeparamref name="TSource"/> to <typeparamref name="TResult"/>.</param>
        /// <param name="page">The page index.</param>
        /// <param name="limit">The page size.</param>
        /// <param name="indexFrom">The start index value.</param>
        /// <returns>An instance of the inherited from <see cref="IPagedList{T}"/> interface.</returns>
        public static IPagedList<TResult> ToPagedList<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int page, int limit, int indexFrom = 0) => new PagedList<TSource, TResult>(source, converter, page, limit, indexFrom);
    }
}
