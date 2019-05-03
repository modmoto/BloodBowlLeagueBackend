using Application.Matches;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Application;
using ServiceConfig;

namespace Host.Matches.Startup
{
    public class Startup
    {
        readonly MicrowaveConfiguration _config = new MicrowaveConfiguration
        {
            ServiceLocations = ServiceConfiguration.ServiceAdresses,
            DatabaseConfiguration = new DatabaseConfiguration { DatabaseName = "Matches" },
            ServiceName = "MatchService"
        };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddMicrowave(_config);

            services.AddTransient<SeasonCreatedEventHandler>();
            services.AddTransient<MatchCommandHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            app.RunMicrowaveQueries();
        }
    }
}