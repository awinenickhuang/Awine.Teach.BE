using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Awine.Framework.Core
{
    public static class LinqDynamicQueryable
    {
        public static class DynamicQueryable
        {
            public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>
            (IQueryable<TSource> query, Expression<Func<TSource, TKey>> keySelector)
            {
                return query.GroupBy(keySelector);
            }
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }
    }
}
