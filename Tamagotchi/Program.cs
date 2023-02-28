using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Tamagotchi.TamagotchiConsoleUi;

namespace Tamagotchi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<LifeCycle>();
                    services.AddHostedService<ConsoleManager>();
                    services.AddScoped<ILifeCycleManager, LifeCycleManager>();
                    services.AddScoped<TamagotchiApi>();
                    services.AddOptions<GameSettings>().Bind(hostContext.Configuration.GetSection("GameSettings"));
                    services.AddOptions<DragonMessages>().Bind(hostContext.Configuration.GetSection("DragonMessages"));
                })
                .Build().RunAsync();
        }
    }
}