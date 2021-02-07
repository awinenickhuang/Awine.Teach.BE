using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.Swagger
{
    /// <summary>
    /// Swagger扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// AddHandDaySwagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceDescribe"></param>
        /// <returns></returns>
        public static IServiceCollection AddHandDaySwagger(this IServiceCollection services, Action<SwaggerOptions> swaggerOptions) => services
            .AddSwaggerGen(options =>
            {
                if (services == null) throw new ArgumentNullException(nameof(services));

                //options.SwaggerDoc(swaggerOptions.Version, new Microsoft.OpenApi.Models.OpenApiInfo
                //{
                //    Title = swaggerOptions.Title,
                //    Version = swaggerOptions.Version,
                //    Description = swaggerOptions.Description
                //});

                //var xmlPath = swaggerOptions.XMLDocument;
                // options.IncludeXmlComments(xmlPath);
            });

        /// <summary>
        /// UseHandDaySwagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceDescribe"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHandDaySwagger(this IApplicationBuilder app, Action<SwaggerOptions> swaggerOptions) => app.UseSwagger(c =>
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
        })
        .UseSwaggerUI(c =>
        {
            // Core
            //c.SwaggerEndpoint($"/swagger/{serviceDescribe.Version}/swagger.json", $"{serviceDescribe.Title} {serviceDescribe.Version}");

            // Display
            c.DefaultModelExpandDepth(2);
            //c.DefaultModelRendering(ModelRendering.Model);
            c.DefaultModelsExpandDepth(-1);
            c.DisplayOperationId();
            c.DisplayRequestDuration();
            //c.DocExpansion(DocExpansion.None);
            c.EnableDeepLinking();
            c.EnableFilter();
            c.ShowExtensions();
        });
    }
}