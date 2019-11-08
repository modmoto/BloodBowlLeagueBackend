using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using ServiceConfig;

namespace Host.Races.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMicrowaveUi();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("https://ka-bbl.herokuapp.com/", "http://localhost:3000/")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddMicrowave(c =>
            {
                c.WithServiceName("RaceService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
            });

            var domainEvents = RaceEventSeeds.Seeds;

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(domainEvents);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMicrowaveUi();
            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}
