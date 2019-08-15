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

            var domainEvents = EventSeedsTeams.Seeds;

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(domainEvents);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMicrowaveUi();
            app.RunMicrowaveQueries();
            app.UseMvc();
        }
    }

    public class EventSeedsTeams
    {
        public static IEnumerable<IDomainEvent> Seeds
        {
            get
            {
                var events = new List<IDomainEvent>
                {
                    new TeamCreated(
                        GuidIdentity.Create(new Guid("772F7E84-4237-4634-AF85-5C0D72FF8DBD")),
                        StringIdentity.Create("Humans"),
                        "Karlsruher Könige",
                        "Walter",
                        HumanPlayers,
                        new GoldCoins(1000000)),
                    new TeamCreated(
                        GuidIdentity.Create(new Guid("38C41447-21F6-4941-BD7E-AC97EF866197")),
                        StringIdentity.Create("Dwarfs"),
                        "Berghausen Brüglerz",
                        "Rahel",
                        DwarfPlayers,
                        new GoldCoins(1000000)),
                    new TeamCreated(
                        GuidIdentity.Create(new Guid("13552A55-D612-40D5-88F9-5106A05CCBAC")),
                        StringIdentity.Create("Dwarfs"),
                        "Rat Bullz",
                        "Silas",
                        DwarfPlayers,
                        new GoldCoins(1000000)),
                    new TeamCreated(
                        GuidIdentity.Create(new Guid("D5BB0FDA-BBE5-4271-8311-460AE5AD3DDA")),
                        StringIdentity.Create("DarkElves"),
                        "Spikey Bits",
                        "Merlin",
                        DarkElvPlayers,
                        new GoldCoins(1000000)),
                };

                events.AddRange(NightElveTeamWithPlayers);
                events.AddRange(HumanTeamWithPlayers);

                return events;
            }
        }

        public static IEnumerable<IDomainEvent> NightElveTeamWithPlayers
        {
            get
            {
                var team1 = GuidIdentity.Create(new Guid("2798435C-9C72-4ECE-BD7D-00BECBACCED7"));

                var events = new List<IDomainEvent>
                {
                    new TeamCreated(
                        team1,
                        StringIdentity.Create("DarkElves"),
                        "Simons Team",
                        "Der Simon",
                        DarkElvPlayers,
                        new GoldCoins(1000000)),
                    new PlayerBought(
                        team1,
                        StringIdentity.Create("DE_LineMan"),
                        GuidIdentity.Create(new Guid("EC48B7FF-B76D-471F-99B0-761EC43C4101")),
                        new GoldCoins(930000)),
                    new PlayerBought(
                        team1,
                        StringIdentity.Create("DE_LineMan"),
                        GuidIdentity.Create(new Guid("C2DEDB29-C59D-4D8F-B854-6B44D04E6C7A")),
                        new GoldCoins(860000)),
                    new PlayerBought(
                        team1,
                        StringIdentity.Create("DE_LineMan"),
                        GuidIdentity.Create(new Guid("E86E63E2-8C3C-4CFF-8719-68BD844CD7F7")),
                        new GoldCoins(790000))
                };

                return events;
            }
        }

        public static IEnumerable<IDomainEvent> HumanTeamWithPlayers
        {
            get
            {
                var team2 = GuidIdentity.Create(new Guid("406D35EE-421A-4D45-9F34-1834D5ACD215"));

                var events = new List<IDomainEvent>
                {
                    new TeamCreated(
                        team2,
                        StringIdentity.Create("Humans"),
                        "Simons Scheiss Team",
                        "Der Simon Poppinga",
                        HumanPlayers,
                        new GoldCoins(1000000)),
                    new PlayerBought(
                        team2,
                        StringIdentity.Create("HU_Blitzer"),
                        GuidIdentity.Create(new Guid("9CF84B11-5852-4D09-BB08-5357E6DA04C8")),
                        new GoldCoins(950000)),
                    new PlayerBought(
                        team2,
                        StringIdentity.Create("HU_LineMan"),
                        GuidIdentity.Create(new Guid("1796B724-B55F-47A3-A498-153379C516EA")),
                        new GoldCoins(900000)),
                };

                return events;
            }
        }

        public static IEnumerable<AllowedPlayer> DarkElvPlayers =>
            new List<AllowedPlayer>
            {
                new AllowedPlayer(StringIdentity.Create("DE_LineMan"), 16, new GoldCoins(70000)),
                new AllowedPlayer(StringIdentity.Create("DE_Assassine"), 2, new GoldCoins(90000)),
                new AllowedPlayer(StringIdentity.Create("DE_Blitzer"), 4, new GoldCoins(100000)),
                new AllowedPlayer(StringIdentity.Create("DE_WitchElve"), 2, new GoldCoins(110000))
            };

        public static IEnumerable<AllowedPlayer> HumanPlayers =>
            new List<AllowedPlayer>
            {
                new AllowedPlayer(StringIdentity.Create("HU_LineMan"), 16, new GoldCoins(50000)),
                new AllowedPlayer(StringIdentity.Create("HU_Blitzer"), 4, new GoldCoins(90000)),
                new AllowedPlayer(StringIdentity.Create("HU_Catcher"), 4, new GoldCoins(70000)),
                new AllowedPlayer(StringIdentity.Create("HU_Thrower"), 2, new GoldCoins(70000)),
                new AllowedPlayer(StringIdentity.Create("HU_Ogre"), 1, new GoldCoins(150000))
            };

        public static IEnumerable<AllowedPlayer> DwarfPlayers =>
            new List<AllowedPlayer>
            {
                new AllowedPlayer(StringIdentity.Create("DW_Blocker"), 16, new GoldCoins(70000)),
                new AllowedPlayer(StringIdentity.Create("DW_Runner"), 2, new GoldCoins(80000)),
                new AllowedPlayer(StringIdentity.Create("DW_Blitzer"), 2, new GoldCoins(80000)),
                new AllowedPlayer(StringIdentity.Create("DW_TrollSlayer"), 2, new GoldCoins(90000)),
                new AllowedPlayer(StringIdentity.Create("DW_DeathRoller"), 1, new GoldCoins(160000))
            };
    }
}
