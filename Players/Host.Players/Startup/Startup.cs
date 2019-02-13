using System;
using Application.Players;
using Domain.Players.Events.ForeignEvents;
using Domain.Players.Events.PlayerConfigs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.EventStores;
using Microwave.Queries;
using Microwave.WebApi;

namespace Host.Players.Startup
{
    public class Startup
    {
        readonly ReadModelConfiguration _readModelConfig = new ReadModelConfiguration(new Uri("http://localhost:5002/"))
        {
            Database = new ReadDatabaseConfig { DatabaseName = "PlayerReadModelDb"},
            DomainEventConfig = new DomainEventConfig
            {
                { typeof(PlayerBought), new Uri("http://localhost:5000/")}
            }
        };

        readonly WriteModelConfiguration _writeModelConfig = new WriteModelConfiguration
        {
            Database = new WriteDatabaseConfig { DatabaseName = "PlayerWriteModelDb"},
        };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddMicrowave(
                _writeModelConfig,
                typeof(PlayerConfigCreated).Assembly);

            services.AddMicrowaveReadModels(
                _readModelConfig,
                typeof(OnPlayerBoughtCreatePlayer).Assembly,
                typeof(PlayerBought).Assembly);

            services.AddTransient<PlayerConfigSeedHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var domainEventFactory = app.ApplicationServices.GetService<IDomainEventFactory>();
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var raceConfigSeedHandler = serviceScope.ServiceProvider.GetService<PlayerConfigSeedHandler>();
                raceConfigSeedHandler.EnsurePlayerConfigSeed().Wait();
            }

            app.RunMicrowaveQueries();
            app.UseMvc();
        }
    }
}
