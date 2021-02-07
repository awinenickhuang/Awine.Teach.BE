using Awine.Framework.AspNetCore.CustomException;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.Extensions
{
    /// <summary>
    /// 统一处理异常信息
    /// </summary>
    public static class IMvcCoreBuilderApiExceptionExtensions
    {
        /// <summary>
        /// 统一处理异常信息
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddMvcExceptionHandling(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });
        }

        /// <summary>
        /// 统一处理异常信息
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcCoreBuilder AddMvcExceptionHandling(this IMvcCoreBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });
        }
    }
}
