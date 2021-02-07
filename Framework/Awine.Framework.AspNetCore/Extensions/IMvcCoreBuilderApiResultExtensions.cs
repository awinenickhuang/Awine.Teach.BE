using Awine.Framework.AspNetCore.ModelVerify;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.Extensions
{
    /// <summary>
    /// Api Extensions
    /// </summary>
    public static class IMvcCoreBuilderApiResultExtensions
    {
        /// <summary>
        /// 格式化API返回值
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddMvcApiResult(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(ModelVerifyNotifyFilter));
            });
        }

        /// <summary>
        /// 格式化API返回值
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcCoreBuilder AddMvcApiResult(this IMvcCoreBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(ModelVerifyNotifyFilter));
            });
        }
    }
}
