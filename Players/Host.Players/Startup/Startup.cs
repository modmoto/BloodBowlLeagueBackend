using Application.Players;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using Microwave.WebApi;
using Microwave.WebApi.Queries;
using ServiceConfig;

namespace Host.Players.Startup
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            var baseAdress = _configuration.GetValue<string>("baseAdress") ?? "http://localhost";

            services.AddMicrowave(config =>
            {
                config.WithFeedType(typeof(EventFeed<>));
            });

            services.AddMicrowaveWebApi(c =>
            {
                c.WithServiceName("PlayerService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdressesFrom(baseAdress));
            });

            services.AddMicrowavePersistenceLayerInMemory();

            services.AddMicrowaveUi();

            services.AddTransient<PlayerCommandHandler>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            app.UseMicrowaveUi();
            app.RunMicrowaveQueries();
            app.RunMicrowaveServiceDiscovery();
            app.UseCors("MyPolicy");
        }
    }
}
