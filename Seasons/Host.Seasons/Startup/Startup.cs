using System;
using System.Collections.Generic;
using Application.Matches;
using Domain.Seasons.Events;
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

            var guidIdentity = GuidIdentity.Create();
            var domainEvents = new List<IDomainEvent>
            {
                new SeasonCreated(guidIdentity, "Meine Neue Season", DateTimeOffset.Now ),
                new TeamAddedToSeason(guidIdentity, GuidIdentity.Create(new Guid("2798435C-9C72-4ECE-BD7D-00BECBACCED7"))),
                new TeamAddedToSeason(guidIdentity, GuidIdentity.Create(new Guid("406D35EE-421A-4D45-9F34-1834D5ACD215"))),
            };

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(domainEvents);
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
}