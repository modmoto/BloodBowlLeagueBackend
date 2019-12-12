using Application.Matches;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using ServiceConfig;

namespace Host.Matches.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

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

            services.AddTransient<OnSeasonStartedCreateMatchesEventHandler>();
            services.AddTransient<MatchCommandHandler>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            app.UseMicrowaveUi();
            app.RunMicrowaveQueries();
            app.UseCors("MyPolicy");
        }
    }
}