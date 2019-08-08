using System;
using System.Collections.Generic;
using Application.Matches;
using Domain.Seasons;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;
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
                var created = Season.Create("Meine gestartete Season");
                var season = new Season();
                season.Apply(created.DomainEvents);
                var addTeam1 = season.AddTeam(GuidIdentity.Create(new Guid("2798435C-9C72-4ECE-BD7D-00BECBACCED7")));
                var addTeam2 = season.AddTeam(GuidIdentity.Create(new Guid("406D35EE-421A-4D45-9F34-1834D5ACD215")));
                var addTeam3 = season.AddTeam(GuidIdentity.Create(new Guid("772F7E84-4237-4634-AF85-5C0D72FF8DBD")));
                var addTeam4 = season.AddTeam(GuidIdentity.Create(new Guid("38C41447-21F6-4941-BD7E-AC97EF866197")));

                season.Apply(addTeam1.DomainEvents);
                season.Apply(addTeam2.DomainEvents);
                season.Apply(addTeam3.DomainEvents);
                season.Apply(addTeam4.DomainEvents);

                var startSeason = season.StartSeason();
                var events = new List<IDomainEvent>();
                events.AddRange(created.DomainEvents);
                events.AddRange(addTeam1.DomainEvents);
                events.AddRange(addTeam2.DomainEvents);
                events.AddRange(addTeam3.DomainEvents);
                events.AddRange(addTeam4.DomainEvents);
                events.AddRange(startSeason.DomainEvents);

                var created2 = Season.Create("Meine frische Season");
                var season2 = new Season();
                season2.Apply(created.DomainEvents);
                var addTeam5 = season2.AddTeam(GuidIdentity.Create(new Guid("2798435C-9C72-4ECE-BD7D-00BECBACCED7")));

                events.AddRange(created2.DomainEvents);
                events.AddRange(addTeam5.DomainEvents);

                return events;
            }
        }
    }
}