using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Awine.Framework.AspNetCore;
using Awine.Framework.AspNetCore.Authorize;
using Awine.Framework.AspNetCore.Consul;
using Awine.Framework.AspNetCore.Extensions;
using Awine.Teach.FoundationService.Application.Extensions;
using Awine.Teach.FoundationService.Configurations;
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
using Microsoft.IdentityModel.Tokens;

namespace Awine.Teach.FoundationService
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
                //全局配置Json序列化处理
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(SerializerHelper.JsonTimeConverter());
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            services.AddFoundationMySQLProvider(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("MySQLConnection");
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // AutoMapper Settings
            services.AddAutoMapperSetup();

            //services.AddAutoMapper();
            services.AddFoundationApplication();

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

            //CAP Settings
            services.AddCAPSetup(Configuration);
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

            //身份验证
            app.UseAuthentication();

            //授权
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
