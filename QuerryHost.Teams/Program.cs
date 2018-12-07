using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microwave.DependencyInjectionExtensions;

namespace QuerryHost.Teams
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            using (var serviceScope = webHost.Services.CreateScope())
            {
                var asyncEventDelegator = serviceScope.ServiceProvider.GetService<AsyncEventDelegator>();
                Task.Run(() =>
                {
                    Task.Delay(10000).Wait();
                    asyncEventDelegator.Update();
                });
                webHost.Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}