using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Tamagotchi.Controllers;

namespace Tamagotchi.IntegrationTest
{
    public class TamagotchiApiShould
    {
        [Fact]
        public async Task StartGameWhenNameIsProvided()
        {
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon1";

            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);

            responseToStartGame.EnsureSuccessStatusCode();

            responseToStartGame.StatusCode.Should().Be(HttpStatusCode.OK);
            dragonId.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ReturnGameStatusWhenDragonIdIsProvided()
        {
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon2";
            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);
            Thread.Sleep(1000);
            var responseToGetGameStatus = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToGetGameStatus = await responseToGetGameStatus.Content.ReadAsStringAsync();
            var gameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToGetGameStatus);

            responseToGetGameStatus.EnsureSuccessStatusCode();

            responseToGetGameStatus.StatusCode.Should().Be(HttpStatusCode.OK);
            gameStatus.Should().NotBeNull();
            gameStatus.Name.Should().Be(dragonName);
            gameStatus.Age.Should().BeGreaterThan(0.00);
            gameStatus.Feedometer.Should().BeLessThan(199);
            gameStatus.Happiness.Should().BeLessThan(199);
            gameStatus.AgeGroup.Should().Be(AgeGroup.Baby);
            gameStatus.DragonId.Should().Be(dragonId);
        }
    }
}