using System.Collections.Generic;
using Application.Players;
using Domain.Players;
using Domain.Players.Events.PlayerConfigs;
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

namespace Host.Players.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMicrowave(c =>
            {
                c.WithServiceName("PlayerService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
            });

            var domainEvents = new List<IDomainEvent>
            {
                new PlayerConfigCreated(StringIdentity.Create("DE_LineMan"),
                    new List<StringIdentity>(),
                    new List<SkillType>
                    {
                        SkillType.General
                    },
                    new List<SkillType>
                    {
                        SkillType.General
                    }
                ),
                new PlayerConfigCreated(StringIdentity.Create("DE_Blitzer"),
                    new List<StringIdentity>
                    {
                        Skills.Block
                    },
                    new List<SkillType>
                    {
                        SkillType.General
                    },
                    new List<SkillType>
                    {
                        SkillType.General
                    }
                ),
                new PlayerConfigCreated(StringIdentity.Create("DE_WitchElve"),
                    new List<StringIdentity>
                    {
                        Skills.Dodge
                    },
                    new List<SkillType>
                    {
                        SkillType.General
                    },
                    new List<SkillType>
                    {
                        SkillType.General
                    }
                )
            };

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(domainEvents);
            });

            services.AddMicrowaveUi();

            services.AddTransient<PlayerCommandHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.RunMicrowaveQueries();
            app.UseMicrowaveUi();
            app.UseMvc();
        }
    }
}
