using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Awine.Framework.AspNetCore.Consul;
using Awine.Teach.DocumentService.Configurations;
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

            services.AddCors(options =>
            {
                options.AddPolicy("awinefileservice",
                builder => builder.AllowAnyOrigin()
                .WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                );
            });

            /*
            //�ر���JWT��Claim ����ӳ��, �Ա�����well-known claims ��֤����������κδ�Authorization Server���ص�Claims
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //����֤����ע�ᵽDI, ��������Bearer��ΪĬ��ģʽ
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>//��DIע����token��֤�Ĵ�����
                {
                    options.Authority = Configuration["AuthenticationCenter:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = Configuration["AuthenticationCenter:ApiName"]; ;
                    options.ApiSecret = Configuration["AuthenticationCenter:ApiSecret"]; ;
                    //����ʱ��ƫ�ƣ�Ĭ��5S
                    options.JwtValidationClockSkew = TimeSpan.FromSeconds(0);
                });
             */

            //Add service discovery with Consul
            services.AddAwineConsul(Configuration);
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("awinefileservice");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();

            app.UseSwaggerSetup();

            // Use service discovery with Consul
            app.UseConsulRegisterService(Configuration);
        }
    }
}
