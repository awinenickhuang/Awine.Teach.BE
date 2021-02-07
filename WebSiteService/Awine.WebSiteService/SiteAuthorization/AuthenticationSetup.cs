using Awine.WebSiteService.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.WebSiteService.SiteAuthorization
{
    public static class AuthenticationSetup
    {
        public static void AddAuthenticationSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = new JwtSetting();
            configuration.Bind("JwtSetting", jwtSetting);

            if (services == null) throw new ArgumentNullException(nameof(services));

            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       //ValidIssuer = jwtSetting.Issuer,
                       //ValidAudience = jwtSetting.Audience,
                       //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey)),
                       //// 默认 300s
                       //ClockSkew = TimeSpan.Zero

                       ValidateIssuer = true,//是否验证Issuer
                       ValidateAudience = true,//是否验证Audience
                       ValidateLifetime = true,//是否验证失效时间
                       ClockSkew = TimeSpan.FromSeconds(30),
                       ValidateIssuerSigningKey = true,//是否验证SecurityKey
                       ValidAudience = jwtSetting.Audience,//Audience
                       ValidIssuer = jwtSetting.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey))//拿到SecurityKey
                   };
               });
        }
    }
}
