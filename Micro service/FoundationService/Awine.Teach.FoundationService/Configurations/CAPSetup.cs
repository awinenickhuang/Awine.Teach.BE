using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Configurations
{
    /// <summary>
    /// CAP
    /// </summary>
    public static class CAPSetup
    {
        /// <summary>
        /// Add CAP Setup
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCAPSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services.AddCap(option =>
            //{
            //    option.UseMySql(configuration.GetConnectionString("MySQLConnection"));

            //    option.UseRabbitMQ(config =>
            //    {
            //        config.HostName = configuration["CAP:RabbitMQ:Host"];
            //        config.VirtualHost = configuration["CAP:RabbitMQ:VirtualHost"];
            //        config.Port = Convert.ToInt32(configuration["CAP:RabbitMQ:Port"]);
            //        config.UserName = configuration["CAP:RabbitMQ:UserName"];
            //        config.Password = configuration["CAP:RabbitMQ:Password"];
            //    });

            //    option.FailedRetryCount = 2;
            //    option.FailedRetryInterval = 5;
            //});

            //services.Add(ServiceDescriptor.Singleton<IAspnetUserSubscriberService, AspnetUserSubscriberService>());
        }
    }
}
