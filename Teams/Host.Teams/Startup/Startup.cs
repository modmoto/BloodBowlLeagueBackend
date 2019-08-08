using System;
using System.Collections.Generic;
using Application.Teams;
using Application.Teams.RaceConfigSeed;
using Domain.Teams;
using Domain.Teams.DomainEvents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;
using Microwave.EventStores.SnapShots;
using Microwave.Persistence.InMemory;
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

            var domainEvents = new List<IDomainEvent>
            {
                new TeamCreated(
                    GuidIdentity.Create(new Guid("2798435C-9C72-4ECE-BD7D-00BECBACCED7")), 
                    StringIdentity.Create("NightElves"), 
                    "Simons Team",
                    "Der Simon",
                    new List<AllowedPlayer>(),
                    new GoldCoins(1000000)),
                new TeamCreated(
                    GuidIdentity.Create(new Guid("406D35EE-421A-4D45-9F34-1834D5ACD215")),
                    StringIdentity.Create("Humans"),
                    "Simons Scheiss Team",
                    "Der Simon Poppinga",
                    new List<AllowedPlayer>(),
                    new GoldCoins(1000000)),
                new TeamCreated(
                    GuidIdentity.Create(new Guid("13552A55-D612-40D5-88F9-5106A05CCBAC")),
                    StringIdentity.Create("Dwarfes"),
                    "Rat Bullz",
                    "Silas",
                    new List<AllowedPlayer>(),
                    new GoldCoins(1000000))
            };

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(domainEvents);
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
