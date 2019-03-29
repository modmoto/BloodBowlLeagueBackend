using Application.Teams;
using Application.Teams.RaceConfigSeed;
using Domain.Teams.DomainEvents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Application;

namespace Teams.WriteHost.Startup
{
    public class Startup
    {
        readonly MicrowaveConfiguration _writeModelConfig = new MicrowaveConfiguration
        {
            WriteDatabase = new WriteDatabaseConfig { DatabaseName = "TeamWriteModelDb"}
        };
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<TeamCommandHandler>();
            services.AddTransient<RaceConfigSeedHandler>();

            services.AddMicrowave(_writeModelConfig, typeof(TeamCreated).Assembly);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var raceConfigSeedHandler = serviceScope.ServiceProvider.GetService<RaceConfigSeedHandler>();
                raceConfigSeedHandler.EnsureRaceConfigSeed().Wait();
            }

            app.UseMvc();
        }
    }
}
