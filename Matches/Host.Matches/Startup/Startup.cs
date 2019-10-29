using Application.Matches;
using ServiceConfig;

namespace Host.Matches.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMicrowaveUi();

            services.AddMicrowave(c =>
            {
                c.WithServiceName("MatchService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
            });

            services.AddMicrowavePersistenceLayerInMemory(c =>
            {
                c.WithEventSeeds(EventSeeds.Seeds);
            });

            services.AddTransient<OnSeasonStartedCreateMatchesEventHandler>();
            services.AddTransient<MatchCommandHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            app.UseMicrowaveUi();
            app.RunMicrowaveQueries();
        }
    }
}