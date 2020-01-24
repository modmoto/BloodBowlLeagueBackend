using Application.Players;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Logging;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using Microwave.WebApi;
using Microwave.WebApi.Queries;
using ServiceConfigNew;

namespace Host.Players.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors().AddMvc();

            services.AddMicrowave(config =>
            {
                config.WithFeedType(typeof(EventFeed<>))
                    .WithLogLevel(MicrowaveLogLevel.Info);
            });

            services.AddMicrowaveWebApi(c =>
            {
                c.WithServiceName("PlayerService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
            });

            services.AddMicrowavePersistenceLayerInMemory();

            services.AddMicrowaveUi();

            services.AddTransient<PlayerCommandHandler>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(
                options => options
                    .WithOrigins(
                        "http://localhost:3000",
                        "http://*.blood-bowl-league.com",
                        "http://blood-bowl-league.com")
                    .AllowAnyMethod()
            );
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            app.UseMicrowaveUi();
            app.RunMicrowaveQueries();
            app.RunMicrowaveServiceDiscovery();
            app.UseCors("MyPolicy");
        }
    }
}
