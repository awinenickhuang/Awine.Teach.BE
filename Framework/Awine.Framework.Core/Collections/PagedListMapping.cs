using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.Core.Collections
{
    /// <summary>
    /// PagedList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedListMapping<T> : IPagedList<T>
    {
        /// <summary>
        /// IndexFrom
        /// </summary>
        public int IndexFrom { get; set; }

        /// <summary>
        /// Page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// TotalPages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Items
        /// </summary>
        public IList<T> Items { get; set; }

        /// <summary>
        /// HasPreviousPage
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// HasNextPage
        /// </summary>
        public bool HasNextPage { get; set; }
    }
}
