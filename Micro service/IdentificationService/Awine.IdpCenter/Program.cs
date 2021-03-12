using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Awine.IdpCenter
{
    /// <summary>
    /// 入口文件
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 程序入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create Host Builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
           {
               configurationBuilder
                   .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                   .AddJsonFile("appsettings.json", true, true)
                   .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                   .AddEnvironmentVariables();
           })
           .ConfigureWebHostDefaults(webBuilder =>
           {
               //webBuilder.ConfigureKestrel(options =>
               //{
               //    options.Listen(IPAddress.IPv6Any, 443, listenOptions =>
               //    {
               //        listenOptions.UseHttps("www.cdzssy.cn.pfx", "cdzSsy@2020$%^");
               //    });
               //});
               webBuilder.UseStartup<Startup>();
           });
    }
}
