using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microwave;
using Microwave.Queries;
using Teams.ReadHost.Players;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Startup
{
    public class Startup
    {
        readonly ReadModelConfiguration _readModelConfig = new ReadModelConfiguration(new Uri("http://localhost:5000/"))
        {
            Database = new ReadDatabaseConfig { DatabaseName = "TeamReadModelDb"},
            ReadModelConfig = new ReadModelConfig
            {
                { typeof(PlayerReadModel), new Uri("http://localhost:5002/")}
            }
        };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMicrowaveReadModels(_readModelConfig, Assembly.GetAssembly(typeof(TeamReadModel)));

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.RunMicrowaveQueries();

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");

            app.UseMvc();
        }
    }
}