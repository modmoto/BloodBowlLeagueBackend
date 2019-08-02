using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Persistence.MongoDb;
using Microwave.Queries.Handler;
using Microwave.UI;
using ServiceConfig;

namespace Seasons.ReadHost.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMicrowaveUi();

            services.AddMicrowave(c =>
            {
                c.WithServiceName("SeasonsQuerryService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
            });

            services.AddMicrowavePersistenceLayerMongoDb(c =>
            {
                c.WithDatabaseName("SeasonsReadModelDb");
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
//            var requestDelegate = app.Build();
//            var queryEventHandler = app.ApplicationServices.GetServices<IQueryEventHandler>();
//
//            var eventHandler = queryEventHandler.First();
//            eventHandler.Update().Wait();
            app.RunMicrowaveQueries();
            app.UseMicrowaveUi();
            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}