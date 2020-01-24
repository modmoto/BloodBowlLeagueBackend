using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Logging;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using Microwave.WebApi;
using Microwave.WebApi.Queries;
using ServiceConfigNew;

namespace Teams.ReadHost.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors()
                .AddMvc()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                ;

            services.AddMicrowave(config =>
            {
                config.WithFeedType(typeof(EventFeed<>))
                    .WithLogLevel(MicrowaveLogLevel.Info);
            });

            services.AddMicrowaveWebApi(c =>
            {
                c.WithServiceName("TeamsQuerryService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
            });

            services.AddMicrowavePersistenceLayerInMemory();
            services.AddMicrowaveUi();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCors(
                options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
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