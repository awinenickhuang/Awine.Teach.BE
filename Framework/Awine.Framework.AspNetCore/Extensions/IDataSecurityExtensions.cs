using Awine.Framework.AspNetCore.DataSecurity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.Extensions
{
    public static class IDataSecurityExtensions
    {
        public static IMvcBuilder AddDataSecurityHandling(this IMvcBuilder builder, IConfiguration configuration)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddMvcOptions(options =>
            {
                options.Filters.Add(typeof(DataDecryptFilter));
            });
        }
    }
}
