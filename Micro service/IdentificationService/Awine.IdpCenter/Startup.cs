using Awine.IdpCenter.Configuration;
using Awine.IdpCenter.Entities;
using Awine.IdpCenter.ExtensionGrants;
using Awine.IdpCenter.Services;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.WeChat;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Awine.IdpCenter
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                             ForwardedHeaders.XForwardedFor |
                             ForwardedHeaders.XForwardedHost |
                             ForwardedHeaders.XForwardedProto;

                options.ForwardLimit = 2;  //Limit number of proxy hops trusted
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            if (Environment.IsDevelopment())
            {
                //开发环境下不使用HTTPS
                services.ConfigureNonBreakingSameSiteCookies();
            }
            else
            {
                // 生产环境下禁用日志的控制台输出，防止由于线程同步造成的性能损失
                services.AddLogging(configure => configure.ClearProviders());
            }

            services.AddIdentityServer(options =>
            {
                options.IssuerUri = "https://uiac.cdzssy.cn";
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                // 获取或设置用户交互的选项
                //options.UserInteraction = new IdentityServer4.Configuration.UserInteractionOptions()
                //{
                //    LoginUrl = "/oauth2/authorize",
                //};
            })
            .AddJwtBearerClientAuthentication()
            .AddAppAuthRedirectUriValidator()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddProfileService<ProfileService>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = dbContext => dbContext.UseMySql(Configuration.GetConnectionString("MySqlConnectionString"), ServerVersion.FromString("8.0.16 - mysql"),
                builder =>
                {
                    //builder.EnableRetryOnFailure(
                    //    maxRetryCount: 5,
                    //    maxRetryDelay: TimeSpan.FromSeconds(30),
                    //    errorNumbersToAdd: null);
                });
            })
            // (codes, tokens, consents)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = dbContext => dbContext.UseMySql(Configuration.GetConnectionString("MySqlConnectionString"), ServerVersion.FromString("8.0.16 - mysql"),
                builder =>
                {
                    //builder.EnableRetryOnFailure(
                    //    maxRetryCount: 5,
                    //    maxRetryDelay: TimeSpan.FromSeconds(30),
                    //    errorNumbersToAdd: null);
                });
                // 自动清理 token
                options.EnableTokenCleanup = false;
                options.TokenCleanupInterval = 30;
            })
            .AddUserStore(options =>
            {
                options.ConfigureDbContext = dbContext => dbContext.UseMySql(Configuration.GetConnectionString("MySqlConnectionString"), ServerVersion.FromString("8.0.16 - mysql"),
                builder =>
                {
                    //builder.EnableRetryOnFailure(
                    //    maxRetryCount: 5,
                    //    maxRetryDelay: TimeSpan.FromSeconds(30),
                    //    errorNumbersToAdd: null);
                });
            })
            // 开发时使用，生产环境使用正式证书
            //.AddDeveloperSigningCredential()
            //生产环境- > 使用正式证书
            .AddSigningCredential(new X509Certificate2(Path.Combine(AppContext.BaseDirectory, Configuration["Certificates:Path"]), Configuration["Certificates:Password"]));

            //IdentityModelEventSource.ShowPII = true;

            //添加事件总线
            services.AddCap(option =>
            {
                option.UseMySql(Configuration.GetConnectionString("MySqlConnectionString"));

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

            //添加扩展登录方案
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddDingTalk(o =>
            {
                o.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                o.ClientId = Configuration["ExternalDingTalk:AppId"];
                o.ClientSecret = Configuration["ExternalDingTalk:Secret"];
            })
            .AddWeChat(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.ClientId = Configuration["ExternalWeChat:AppId"];
                options.ClientSecret = Configuration["ExternalWeChat:Secret"];
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 调用其他中间件之前，请先在 Startup.Configure 的基础上调用 UseForwardedHeaders 方法。 配置中间件以转接 X-Forwarded - For 和 X-Forwarded - Proto 标头
            //app.UseForwardedHeaders(new ForwardedHeadersOptions
            //{
            //    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            //});

            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseIdentityServer();

            //app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseAuthorization();

            //CSP
            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add(
            //        "Content-Security-Policy",
            //        "script-src 'self' 'unsafe-inline'; " +
            //        "style-src 'self' 'unsafe-inline'; " +
            //        "img-src 'self'");

            //    await next();
            //});

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
