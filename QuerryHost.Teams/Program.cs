using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microwave.DependencyInjectionExtensions;
using Microwave.Queries;

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