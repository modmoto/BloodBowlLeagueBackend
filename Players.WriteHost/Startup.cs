using Application.Players;
using Domain.Players.Events.PlayerConfigs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microwave;

namespace Players.WriteHost
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

            services.AddMicrowave(Configuration, typeof(PlayerConfigCreated).Assembly);
            services.AddMicrowaveReadModels(Configuration, typeof(OnePlayerBoughtCreatePlayer).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var raceConfigSeedHandler = serviceScope.ServiceProvider.GetService<PlayerConfigSeedHandler>();
                raceConfigSeedHandler.EnsureRaceConfigSeed().Wait();
            }

            app.UseMvc();
        }
    }
}
