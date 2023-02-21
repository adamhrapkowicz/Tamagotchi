using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Tamagotchi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<LifeCycle>();
                    services.AddScoped<ILifeCycleManager, LifeCycleManager>();
                    services.AddScoped<IConsoleManager, ConsoleManager>();
                    services.AddOptions<GameSettings>().Bind(hostContext.Configuration.GetSection("GameSettings"));
                    services.AddOptions<DragonMessages>().Bind(hostContext.Configuration.GetSection("DragonMessages"));
                })
                .Build().RunAsync();
        }
    }
}