using System;
using System.Collections.Generic;
using Application.Matches;
using Domain.Seasons;
using Domain.Seasons.Events;
using Domain.Seasons.TeamReadModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Domain.EventSourcing;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using ServiceConfig;

namespace Host.Matches.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddMicrowave(c =>
            {
                c.WithServiceName("SeasonService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
            });

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(SeasonEvents.Seeds);
            });

            services.AddMicrowaveUi();

            services.AddTransient<SeasonCommandHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            app.RunMicrowaveQueries();
            app.UseMicrowaveUi();
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