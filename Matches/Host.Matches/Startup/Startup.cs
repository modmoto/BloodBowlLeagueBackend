using System;
using System.Collections.Generic;
using Application.Matches;
using Domain.Matches.Events;
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

            services.AddMicrowaveUi();

            services.AddMicrowave(c =>
            {
                c.WithServiceName("MatchService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
            });

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(EventSeeds.Seeds);
            });

            services.AddTransient<SeasonCreatedEventHandler>();
            services.AddTransient<MatchCommandHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            app.UseMicrowaveUi();
            app.RunMicrowaveQueries();
        }
    }

    public class EventSeeds
    {
        public static IEnumerable<IDomainEvent> Seeds
        {
            get
            {
                var matchCreated = new MatchCreated(
                    GuidIdentity.Create(new Guid("8A8CF5CC-1027-44BF-A9B7-B294856396B0")),
                    GuidIdentity.Create(new Guid("2798435C-9C72-4ECE-BD7D-00BECBACCED7")),
                    GuidIdentity.Create(new Guid("406D35EE-421A-4D45-9F34-1834D5ACD215"))
                );

                var matchStarted = new MatchStarted(
                    matchCreated.MatchId,
                    new List<GuidIdentity>
                    {

                        GuidIdentity.Create(new Guid("EC48B7FF-B76D-471F-99B0-761EC43C4101")),
                        GuidIdentity.Create(new Guid("C2DEDB29-C59D-4D8F-B854-6B44D04E6C7A")),
                        GuidIdentity.Create(new Guid("E86E63E2-8C3C-4CFF-8719-68BD844CD7F7")),
                    },
                    new List<GuidIdentity>
                    {
                        GuidIdentity.Create(new Guid("9CF84B11-5852-4D09-BB08-5357E6DA04C8")),
                        GuidIdentity.Create(new Guid("1796B724-B55F-47A3-A498-153379C516EA")),
                    });

                return new List<IDomainEvent> { matchCreated, matchStarted };
            }
        }
    }
}