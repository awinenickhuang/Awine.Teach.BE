using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Awine.Framework.AspNetCore.Consul;
using Awine.Teach.DocumentService.Configurations;
using Awine.Teach.DocumentService.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Awine.Teach.DocumentService
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
            services.AddControllers();

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue;
            });

            services.Configure<AwineFileOptions>(Configuration.GetSection("AwineFileOptions"));

            // Swagger Config
            services.AddSwaggerSetup();

            services.AddRouting(options => options.LowercaseUrls = true);

            //AddCors
            services.AddCors(options =>
            {
                options.AddPolicy("fileservice_allow_origins", builder =>
                {
                    builder
                    .WithOrigins(Configuration["FileServiceOrigins"].Split(','))
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            //Add TencentCos
            services.Configure<FileUploadOptions>(Configuration.GetSection("Upload"));
            services.Configure<CosUploadOptions>(Configuration.GetSection("Upload"));
            services.AddTencentCos(options =>
            {
                options.SecretId = Configuration["TencentCos:SecretId"];
                options.SecretKey = Configuration["TencentCos:SecretKey"];
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["AuthenticationCenter:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = Configuration["AuthenticationCenter:ApiName"];
                });

            //Add service discovery with Consul
            //services.AddAwineConsul(Configuration);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("fileservice_allow_origins");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();

            app.UseSwaggerSetup();

            // Use service discovery with Consul
            //app.UseConsulRegisterService(Configuration);
        }
    }
}
