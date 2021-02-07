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
        /// PageIndex
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }

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
