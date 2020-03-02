using System;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Logging;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using Microwave.WebApi;
using Microwave.WebApi.Queries;

namespace Teams.ReadHost.Startup
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
            services
                .AddCors()
                .AddMvc()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:6008";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "api1";
                });

            var baseAdress = _configuration.GetValue<string>("baseAdresses");
            var serviceUrls = baseAdress.Split(';').Select(s => new Uri(s));

            services.AddMicrowave(config =>
            {
                config.WithFeedType(typeof(EventFeed<>));
                config.WithLogLevel(MicrowaveLogLevel.Trace);
            });

            services.AddMicrowaveWebApi(c =>
            {
                c.WithServiceName("TeamsQuerryService");
                c.ServiceLocations.AddRange(serviceUrls);
            });

            services.AddMicrowavePersistenceLayerInMemory();
            services.AddMicrowaveUi();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCors(
                options => options
                    .WithOrigins(
                        "http://localhost:3000",
                        "http://localhost:80",
                        "http://localhost",
                        "http://*.blood-bowl-league.com",
                        "http://blood-bowl-league.com")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            app.UseMicrowaveUi();
        }
    }
}