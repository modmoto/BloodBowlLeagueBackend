using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using ReadHosts.Common;
using ServiceConfig;

namespace Teams.ReadHost.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMicrowaveUi();

            services.AddMicrowave(c =>
            {
                c.WithServiceName("TeamsQuerryService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
            });

            services.AddMicrowavePersistenceLayerInMemory();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddTransient<MessageMitigator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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