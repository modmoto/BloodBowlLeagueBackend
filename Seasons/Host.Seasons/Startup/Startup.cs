using System;
using System.Collections.Generic;
using System.Linq;
using Application.Matches;
using Domain.Seasons;
using Domain.Seasons.Events;
using Domain.Seasons.TeamReadModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Domain.EventSourcing;
using Microwave.Logging;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using Microwave.WebApi;
using Microwave.WebApi.Queries;

namespace Host.Matches.Startup
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
            services.AddCors().AddMvc();

            var baseAdress = _configuration.GetValue<string>("baseAdresses");
            var serviceUrls = baseAdress.Split(';').Select(s => new Uri(s));

            services.AddMicrowave(config =>
            {
                config.WithFeedType(typeof(EventFeed<>));
            });

            services.AddMicrowaveWebApi(c =>
            {
                c.WithServiceName("SeasonService");
                c.ServiceLocations.AddRange(serviceUrls);
            });

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(SeasonEvents.Seeds);
            });

            services.AddMicrowaveUi();

            services.AddTransient<SeasonCommandHandler>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCors(
                options => options
                    .WithOrigins(
                        "http://localhost:3000",
                        "http://localhost:80",
                        "http://localhost",
                        "http://*.blood-bowl-league.com",
                        "http://blood-bowl-league.com")
                    .AllowAnyMethod()
            );
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            app.UseMicrowaveUi();
            app.RunMicrowaveQueries();
            app.RunMicrowaveServiceDiscovery();
        }
    }

    public class SeasonEvents
    {
        public static IEnumerable<IDomainEvent> Seeds
        {
            get
            {
                var created = new SeasonCreated(
                    new Guid("7A097EAE-BE35-4B4D-A23D-98A6B57534F3"),
                    "Gestartete Season",
                    DateTimeOffset.Now);
                var season = new Season();
                season.Apply(created);
                var addTeam1 = season.AddTeam(new TeamReadModel
                {
                    TeamId = new Guid("2798435C-9C72-4ECE-BD7D-00BECBACCED7")
                });
                var addTeam2 = season.AddTeam(new TeamReadModel
                {
                    TeamId = new Guid("406D35EE-421A-4D45-9F34-1834D5ACD215")
                });
                var addTeam3 = season.AddTeam(new TeamReadModel
                {
                    TeamId = new Guid("772F7E84-4237-4634-AF85-5C0D72FF8DBD")
                });
                var addTeam4 = season.AddTeam(new TeamReadModel
                {
                    TeamId = new Guid("38C41447-21F6-4941-BD7E-AC97EF866197")
                });

                season.Apply(addTeam1.DomainEvents);
                season.Apply(addTeam2.DomainEvents);
                season.Apply(addTeam3.DomainEvents);
                season.Apply(addTeam4.DomainEvents);

                var startSeason = season.StartSeason();
                var events = new List<IDomainEvent>();
                events.Add(created);
                events.AddRange(addTeam1.DomainEvents);
                events.AddRange(addTeam2.DomainEvents);
                events.AddRange(addTeam3.DomainEvents);
                events.AddRange(addTeam4.DomainEvents);
                events.AddRange(startSeason.DomainEvents);

                var created2 = new SeasonCreated(
                    new Guid("BF1EAEB0-34A6-4146-92FA-141806F5B8B1"),
                    "Frische Season",
                    DateTimeOffset.Now);
                var season2 = new Season();
                season2.Apply(created2);

                var addTeam5 = season2.AddTeam(new TeamReadModel
                {
                    TeamId = new Guid("D5BB0FDA-BBE5-4271-8311-460AE5AD3DDA")
                });
                var addTeam6 = season2.AddTeam(new TeamReadModel
                {
                    TeamId = new Guid("38C41447-21F6-4941-BD7E-AC97EF866197")
                });

                events.Add(created2);
                events.AddRange(addTeam5.DomainEvents);
                events.AddRange(addTeam6.DomainEvents);

                return events;
            }
        }
    }
}