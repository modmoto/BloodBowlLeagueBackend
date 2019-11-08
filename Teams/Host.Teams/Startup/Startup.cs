using Application.Teams;
using Domain.Teams;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
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

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("https://ka-bbl.herokuapp.com/", "http://localhost:3000/")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

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
            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}
