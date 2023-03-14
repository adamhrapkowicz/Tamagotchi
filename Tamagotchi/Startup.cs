﻿using System.Text.Json.Serialization;
using Tamagotchi.TamagotchiConsoleUi;

namespace Tamagotchi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _env;

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            services
                .AddCors()
                .AddMvc();

            services
                .AddSession()
                .AddHttpContextAccessor();

            services
                .AddHostedService<LifeCycle>()

                .AddSingleton<ILifeCycleManager, LifeCycleManager>()
                .Configure<GameSettings>(_configuration.GetSection("GameSettings"))
                .Configure<DragonMessages>(_configuration.GetSection("DragonMessages"))
                .AddOptions();

            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();
        }

        public void Configure( IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            app.UseSession();
            app
                .UseRouting()
                .UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod())
                .UseEndpoints(endpoints => endpoints.MapControllers());
            
            if (_env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}