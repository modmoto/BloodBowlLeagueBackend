using System;
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

namespace Host.Races.Startup
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
            services.AddMicrowaveUi();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            var baseAdress = _configuration.GetValue<string>("baseAdress") ?? "http://localhost";

            Console.WriteLine(baseAdress);
            services.AddMicrowaveWebApi(c =>
            {
                c.WithServiceName("RaceService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdressesFrom(baseAdress));
            });

            var domainEvents = RaceEventSeeds.Seeds;

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(domainEvents);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            app.UseMicrowaveUi();
            app.UseCors("MyPolicy");
        }
    }
}
