using System;
using Domain.Matches;
using Domain.Matches.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.EventStores;
using Microwave.Queries;

namespace Host.Matches.Startup
{
    public class Startup
    {
        readonly ReadModelConfiguration _readModelConfig = new ReadModelConfiguration(new Uri("http://localhost:5004/"))
        {
            Database = new ReadDatabaseConfig { DatabaseName = "MatchReadModelDb"},
        };

        readonly WriteModelConfiguration _writeModelConfig = new WriteModelConfiguration
        {
            Database = new WriteDatabaseConfig { DatabaseName = "MatchWriteModelDb"}
        };
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddMicrowave(_writeModelConfig, typeof(MatchFinished).Assembly);
            services.AddMicrowaveReadModels(_readModelConfig, typeof(TeamReadModel).Assembly);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
            app.RunMicrowaveQueries();
        }
    }
}