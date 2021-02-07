using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Awine.Framework.AspNetCore;
using Awine.Framework.AspNetCore.Authorize;
using Awine.Framework.AspNetCore.Consul;
using Awine.Framework.AspNetCore.Extensions;
using Awine.Teach.OperationService.Application.Extensions;
using Awine.Teach.OperationService.Configurations;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Awine.Teach.OperationService
{
    /// <summary>
    /// Operation Service Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddMvcApiResult().AddMvcExceptionHandling().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(SerializerHelper.JsonTimeConverter());
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            services.AddOperationServiceMySQLProvider(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("MySQLConnection");
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // AutoMapper Settings
            services.AddAutoMapperSetup();

            //services.AddAutoMapper();
            services.AddOperationServiceApplication();

            // Swagger Config
            services.AddSwaggerSetup();

            //Add service discovery with Consul
            //services.AddAwineConsul(Configuration);

            services.AddAuthentication("Bearer")
               .AddJwtBearer("Bearer", options =>
               {
                   options.Authority = Configuration["AuthenticationCenter:Authority"];
                   options.RequireHttpsMetadata = false;
                   options.Audience = Configuration["AuthenticationCenter:ApiName"];
               });

            services.AddCap(option =>
            {
                option.UseMySql(Configuration.GetConnectionString("MySQLConnection"));

                option.UseRabbitMQ(config =>
                {
                    config.HostName = Configuration["CAP:RabbitMQ:Host"];
                    config.VirtualHost = Configuration["CAP:RabbitMQ:VirtualHost"];
                    config.Port = Convert.ToInt32(Configuration["CAP:RabbitMQ:Port"]);
                    config.UserName = Configuration["CAP:RabbitMQ:UserName"];
                    config.Password = Configuration["CAP:RabbitMQ:Password"];
                });

                option.FailedRetryCount = 2;
                option.FailedRetryInterval = 5;
            });
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

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerSetup();

            // Use service discovery with Consul
            //app.UseConsulRegisterService(Configuration);
        }
    }
}
