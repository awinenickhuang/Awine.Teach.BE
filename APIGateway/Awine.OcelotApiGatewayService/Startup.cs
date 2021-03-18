using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using Ocelot.Provider.Consul;
using Ocelot.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Awine.OcelotApiGatewayService
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// IWebHostEnvironment
        /// </summary>
        public IWebHostEnvironment Environment;

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                //
            }
            else
            {
                // 生产环境下禁用日志的控制台输出，防止由于线程同步造成的性能损失
                services.AddLogging(configure => configure.ClearProviders());
            }

            services.AddControllers();
            services.AddOcelot(Configuration).AddConsul().AddPolly();
            services.AddHttpContextAccessor();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            var config = new OcelotPipelineConfiguration()
            {
                PreErrorResponderMiddleware = async (context, next) =>
                {
                    if (context.Request.Path.Equals(new PathString("/")))
                    {
                        await context.Response.WriteAsync("OK");
                    }
                    else
                    {
                        await next.Invoke();
                    }
                }
            };

            app.UseOcelot(config).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
