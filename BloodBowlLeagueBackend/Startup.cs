using Application.Teams;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microwave.DependencyInjectionExtensions;
using Microwave.EventStores;
using Microwave.Queries;
using Microwave.Subscriptions;
using Querries.Teams;
using WebApi.Teams;

namespace BloodBowlLeagueBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<TeamController>();
            services.AddTransient<TeamCommandHandler>();

            services.AddMyEventStoreDependencies(typeof(TeamQuery).Assembly, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<EventStoreReadContext>();
                context.Database.EnsureCreated();
                var context2 = serviceScope.ServiceProvider.GetRequiredService<EventStoreWriteContext>();
                context2.Database.EnsureCreated();
                var context3 = serviceScope.ServiceProvider.GetRequiredService<QueryStorageContext>();
                context3.Database.EnsureCreated();
                var context4 = serviceScope.ServiceProvider.GetRequiredService<SubscriptionContext>();
                context4.Database.EnsureCreated();
            }

            app.UseMvc();
        }
    }
}
