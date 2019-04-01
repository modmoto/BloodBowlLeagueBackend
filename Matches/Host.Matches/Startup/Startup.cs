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
            ReadDatabase = new ReadDatabaseConfig { DatabaseName = "MatchReadModelDb"},
            ServiceLocations = ServiceConfiguration.ServiceAdresses,
            WriteDatabase = new WriteDatabaseConfig { DatabaseName = "MatchWriteModelDb" },
            ServiceName = "MatchService"
        };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddMicrowave(_config);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            app.RunMicrowaveQueries();
        }
    }
}