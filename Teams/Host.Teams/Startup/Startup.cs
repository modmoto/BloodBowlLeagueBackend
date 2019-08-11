using System;
using System.Collections.Generic;
using Application.Teams;
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
                    GuidIdentity.Create(new Guid("772F7E84-4237-4634-AF85-5C0D72FF8DBD")),
                    StringIdentity.Create("Amazonen"),
                    "Karlsruher Könige",
                    "Walter",
                    new List<AllowedPlayer>(),
                    new GoldCoins(1000000)),
                new TeamCreated(
                    GuidIdentity.Create(new Guid("38C41447-21F6-4941-BD7E-AC97EF866197")),
                    StringIdentity.Create("Orks"),
                    "Berghausen Brüglerz",
                    "Rahel",
                    new List<AllowedPlayer>(),
                    new GoldCoins(1000000)),
                new TeamCreated(
                    GuidIdentity.Create(new Guid("13552A55-D612-40D5-88F9-5106A05CCBAC")),
                    StringIdentity.Create("Dwarfes"),
                    "Rat Bullz",
                    "Silas",
                    new List<AllowedPlayer>(),
                    new GoldCoins(1000000)),
                new TeamCreated(
                    GuidIdentity.Create(new Guid("D5BB0FDA-BBE5-4271-8311-460AE5AD3DDA")),
                    StringIdentity.Create("Nerds"),
                    "Spikey Bits",
                    "Merlin",
                    new List<AllowedPlayer>(),
                    new GoldCoins(1000000)),

                new RaceCreated(StringIdentity.Create("DarkElves"), new List<AllowedPlayer>
                {
                    new AllowedPlayer(StringIdentity.Create("DE_LineMan"), 16, new GoldCoins(70000), "Lineman"),
                    new AllowedPlayer(StringIdentity.Create("DE_Assassine"), 2, new GoldCoins(90000), "Assasine"),
                    new AllowedPlayer(StringIdentity.Create("DE_Blitzer"), 4, new GoldCoins(100000), "Blitzer"),
                    new AllowedPlayer(StringIdentity.Create("DE_WitchElve"), 2, new GoldCoins(110000), "Witch Elve")
                }, "Dark Elves"),

                new RaceCreated(StringIdentity.Create("Humans"), new List<AllowedPlayer>
                {
                    new AllowedPlayer(StringIdentity.Create("HU_LineMan"), 16, new GoldCoins(50000), "Lineman"),
                    new AllowedPlayer(StringIdentity.Create("HU_Blitzer"), 4, new GoldCoins(90000), "Blitzer"),
                    new AllowedPlayer(StringIdentity.Create("HU_Catcher"), 4, new GoldCoins(70000), "Catcher"),
                    new AllowedPlayer(StringIdentity.Create("HU_Thrower"), 2, new GoldCoins(70000), "Thrower"),
                    new AllowedPlayer(StringIdentity.Create("HU_Ogre"), 1, new GoldCoins(70000), "Ogre")
                }, "Humans")
            };

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(domainEvents);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMicrowaveUi();
            app.UseMvc();
        }
    }
}
