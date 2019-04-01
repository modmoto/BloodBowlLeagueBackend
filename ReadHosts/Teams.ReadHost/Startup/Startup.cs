﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Application;
using ServiceConfig;

namespace Teams.ReadHost.Startup
{
    public class Startup
    {
        readonly MicrowaveConfiguration _config = new MicrowaveConfiguration
        {
            ReadDatabase = new ReadDatabaseConfig { DatabaseName = "TeamReadModelDb"},
            ServiceLocations = ServiceConfiguration.ServiceAdresses,
            ServiceName = "TeamsQuerryService"
        };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMicrowave(_config);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.RunMicrowaveQueries();
            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}