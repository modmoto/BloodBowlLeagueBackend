using System;
using System.Linq;
using Application.Matches;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Logging;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using Microwave.WebApi;
using Microwave.WebApi.Queries;

namespace Host.Matches.Startup
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

            var baseAdress = _configuration.GetValue<string>("baseAdresses");
            var serviceUrls = baseAdress.Split(';').Select(s => new Uri(s));

            services.AddMicrowave(config =>
            {
                config.WithFeedType(typeof(EventFeed<>))
                    .WithLogLevel(MicrowaveLogLevel.Info);
            });

            services.AddMicrowaveWebApi(c =>
            {
                c.WithServiceName("MatchService");
                c.ServiceLocations.AddRange(serviceUrls);
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
            app.RunMicrowaveServiceDiscovery();
            app.UseCors("MyPolicy");
        }
    }
}