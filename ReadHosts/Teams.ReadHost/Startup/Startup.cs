using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.EventStores;
using Microwave.Queries;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Startup
{
    public class Startup
    {
        readonly ReadModelConfiguration _readModelConfig = new ReadModelConfiguration
        {
            Database = new ReadDatabaseConfig { ConnectionString = "mongodb://mongo/", DatabaseName = "TeamReadModelDb"},
        };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMicrowave(new WriteModelConfiguration(), Assembly.GetAssembly(typeof(TeamReadModel)));
            services.AddMicrowaveReadModels(_readModelConfig, Assembly.GetAssembly(typeof(TeamReadModel)));

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
            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}