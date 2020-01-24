using Application.Teams;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Logging;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using Microwave.WebApi;
using Microwave.WebApi.Queries;
using ServiceConfigNew;

namespace Teams.WriteHost.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors().AddMvc();
            services.AddTransient<TeamCommandHandler>();
            services.AddMicrowaveUi();

            services.AddMicrowave(config =>
            {
                config.WithFeedType(typeof(EventFeed<>))
                    .WithLogLevel(MicrowaveLogLevel.Info);
            });

            services.AddMicrowaveWebApi(c =>
            {
                c.WithServiceName("TeamService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
            });

            var domainEvents = EventSeedsTeams.Seeds;

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(domainEvents);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCors(
                options => options
                    .WithOrigins(
                        "http://localhost:3000",
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
}
