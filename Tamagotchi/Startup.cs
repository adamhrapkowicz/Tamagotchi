namespace Tamagotchi
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            app
                .UseRouting()
                .UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod())
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        
    }
}
