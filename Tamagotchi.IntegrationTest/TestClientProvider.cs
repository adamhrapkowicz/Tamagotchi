using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Tamagotchi.IntegrationTest;

public class TestClientProvider : IDisposable
{
    public TestClientProvider()
    {
        var server = new TestServer(
            new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config
                        .AddJsonFile("appsettings.json")
                        .AddEnvironmentVariables();
                }));

        Client = server.CreateClient();
    }

    public HttpClient Client { get; }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}