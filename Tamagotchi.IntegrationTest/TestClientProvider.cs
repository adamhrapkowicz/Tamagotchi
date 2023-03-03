using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Tamagotchi.IntegrationTest
{
    public class TestClientProvider : IDisposable
    {
        private readonly TestServer _server;
        public HttpClient Client { get; private set; }

        public TestClientProvider()
        {
            _server = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        config
                            .AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables();
                    }));

            Client = _server.CreateClient();

        }

        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();
        }
    }
}
