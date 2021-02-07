using AutoMapper;
using Awine.Teach.FoundationService.Application.AutoMappr;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Configurations
{
    /// <summary>
    /// AutoMapper Setup
    /// </summary>
    public static class AutoMapperSetup
    {
        /// <summary>
        /// Add AutoMapper Setup
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(FoundationServiceMapping));
        }
    }
}
