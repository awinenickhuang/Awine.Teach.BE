using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Awine.Framework.AspNetCore.Consul.Extensions
{
    /// <summary>
    /// Uri 扩展
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetPath(this Uri uri)
        {
            return uri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetHostAndPath(this Uri uri)
        {
            return uri.GetComponents(UriComponents.Host | UriComponents.Path, UriFormat.Unescaped);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetSchemeAndHost(this Uri uri)
        {
            return uri.GetComponents(UriComponents.Scheme | UriComponents.Host, UriFormat.Unescaped);
        }

        private static string TrimAfter(this string s, string trim)
        {
            int index = s.IndexOf(trim, StringComparison.Ordinal);
            if (index > 0)
            {
                s = s.Substring(0, index);
            }

            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool StartsWithSegments(this Uri uri, string path)
        {
            // support root path
            bool isRootPath = string.IsNullOrEmpty(path) || path == "/";
            if (isRootPath)
            {
                return uri.Segments.Length == 1;
            }

            // trim query string & separators
            path = path.TrimAfter("?").Trim('/');
            var pathSegments = path.Split('/');

            // skip starting path & trim separators
            var segments = uri.Segments.Skip(1).Select(x => x.Trim('/'));

            return segments.StartsWith(pathSegments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        private static bool StartsWith<T>(this IEnumerable<T> left, IEnumerable<T> right, IEqualityComparer<T> comparer = null)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }

            using (var leftEnumerator = left.GetEnumerator())
            using (var rightEnumerator = right.GetEnumerator())
            {
                for (; ; )
                {
                    if (!rightEnumerator.MoveNext())
                    {
                        return true;
                    }
                    if (!leftEnumerator.MoveNext())
                    {
                        return false;
                    }
                    if (!comparer.Equals(leftEnumerator.Current, rightEnumerator.Current))
                    {
                        return false;
                    }
                }
            }
        }
    }
}
