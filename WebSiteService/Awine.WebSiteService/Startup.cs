using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Framework.AspNetCore;
using Awine.Framework.AspNetCore.Extensions;
using Awine.WebSite.Applicaton.Extensions;
using Awine.WebSiteService.Configurations;
using Awine.WebSiteService.SiteAuthorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Awine.WebSiteService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });

            services.AddWebSiteMySQLProvider(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("MySQLConnection");
            });

            services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapperSetup();

            services.AddWebSiteExtensions();

            services.AddHttpContextAccessor();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddAuthenticationSetup(Configuration);
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

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            ///Ìí¼ÓjwtÑéÖ¤
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
