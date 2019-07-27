using System.Collections.Generic;
using Application.Teams;
using Application.Teams.RaceConfigSeed;
using Domain.Teams;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.EventStores.SnapShots;
using Microwave.Persistence.MongoDb;
using Microwave.UI;
using ServiceConfig;

namespace Teams.WriteHost.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<TeamCommandHandler>();
            services.AddTransient<RaceConfigSeedHandler>();
            services.AddMicrowaveUi();

            services.AddMicrowave(c =>
            {
                c.WithServiceName("TeamService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
                c.SnapShots.Add(new SnapShot<Team>(3));
            });

            services.AddMicrowavePersistenceLayerMongoDb(c =>
            {
                c.WithDatabaseName("Teams");
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var raceConfigSeedHandler = serviceScope.ServiceProvider.GetService<RaceConfigSeedHandler>();
                raceConfigSeedHandler.EnsureRaceConfigSeed().Wait();
            }

            app.UseMicrowaveUi();
            app.UseMvc();
        }
    }
}
