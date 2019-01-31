using Application.Teams;
using Application.Teams.RaceConfigSeed;
using Domain.Teams.DomainEvents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microwave;

namespace Teams.WriteHost.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<TeamCommandHandler>();
            services.AddTransient<RaceConfigSeedHandler>();

            services.AddMicrowave(Configuration, typeof(TeamCreated).Assembly);
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
