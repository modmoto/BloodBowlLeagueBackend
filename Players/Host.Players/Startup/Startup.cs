﻿using Application.Players;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Persistence.InMemory;
using Microwave.UI;
using ServiceConfig;

namespace Host.Players.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMicrowave(c =>
            {
                c.WithServiceName("PlayerService");
                c.ServiceLocations.AddRange(ServiceConfiguration.ServiceAdresses);
            });

            services.AddMicrowavePersistenceLayerInMemory();

            services.AddMicrowaveUi();

            services.AddTransient<PlayerCommandHandler>();
            services.AddTransient<NameService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.RunMicrowaveQueries();
            app.UseMicrowaveUi();
            app.UseMvc();
        }
    }
}
