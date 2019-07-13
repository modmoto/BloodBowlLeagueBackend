using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Persistence.MongoDb;
using Microwave.UI;
using ServiceConfig;

namespace Teams.ReadHost.Startup
{
    public class Startup
    {
        readonly MicrowaveConfiguration _config = new MicrowaveConfiguration
        {
            ServiceLocations = ServiceConfiguration.ServiceAdresses,
            ServiceName = "TeamsQuerryService"
        };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMicrowave(_config,  new MongoDbPersistenceLayer {
                MicrowaveMongoDb = new MicrowaveMongoDb { DatabaseName = "AllReadModelDb"}});
            services.AddMicrowaveUi();

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
            app.RunMicrowaveQueries();
            app.UseMicrowaveUi();
            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}