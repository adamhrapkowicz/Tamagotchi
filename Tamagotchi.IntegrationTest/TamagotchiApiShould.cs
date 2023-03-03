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
            
            var responseToGetGameStatus = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToGetGameStatus = await responseToGetGameStatus.Content.ReadAsStringAsync();
            var gameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToGetGameStatus);

            responseToGetGameStatus.EnsureSuccessStatusCode();

            responseToGetGameStatus.StatusCode.Should().Be(HttpStatusCode.OK);
            gameStatus.Should().NotBeNull();
            gameStatus.Name.Should().Be(dragonName);
            gameStatus.AgeGroup.Should().Be(AgeGroup.Baby);
            gameStatus.DragonId.Should().Be(dragonId);
        }

        [Fact]
        public async Task ReturnDifferentGameStatusAfterTimePasses()
        {
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon3";
            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);
            
            Thread.Sleep(1000);

            var responseToFirstGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToFirstGetGameStatusRequest = await responseToFirstGetGameStatusRequest.Content.ReadAsStringAsync();
            var firstGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToFirstGetGameStatusRequest);

            responseToFirstGetGameStatusRequest.EnsureSuccessStatusCode();

            Thread.Sleep(10000);

            var responseToSecondGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToSecondGetGameStatusRequest = await responseToSecondGetGameStatusRequest.Content.ReadAsStringAsync();
            var secondGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToSecondGetGameStatusRequest);

            responseToSecondGetGameStatusRequest.EnsureSuccessStatusCode();

            responseToFirstGetGameStatusRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            responseToSecondGetGameStatusRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            firstGameStatus.Should().NotBeNull();
            secondGameStatus.Should().NotBeNull();
            firstGameStatus.Name.Should().Be(secondGameStatus.Name);
            firstGameStatus.DragonId.Should().Be(secondGameStatus.DragonId);
            firstGameStatus.Age.Should().BeLessThan(secondGameStatus.Age);
            firstGameStatus.Happiness.Should().BeGreaterThan(secondGameStatus.Happiness);
            firstGameStatus.Feedometer.Should().BeGreaterThan(secondGameStatus.Feedometer);
        }

        [Fact]
        public async Task ReturnIncreasedFeedometerAfterFeedDragonRequest()
        {
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon4";

            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);

            var responseToFirstGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToFirstGetGameStatusRequest = await responseToFirstGetGameStatusRequest.Content.ReadAsStringAsync();
            var firstGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToFirstGetGameStatusRequest);

            responseToFirstGetGameStatusRequest.EnsureSuccessStatusCode();
            
            var responseToFeedDragon = await client.PutAsync($"TamagotchiApi/feed/{dragonId}", new StringContent(""));
            var contentOfTheResponseToFeedDragon = await responseToFeedDragon.Content.ReadAsStringAsync();
            var feedDragonResponse = JsonConvert.DeserializeObject<FeedDragonResponse>(contentOfTheResponseToFeedDragon);

            responseToFeedDragon.EnsureSuccessStatusCode();

            var responseToSecondGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToSecondGetGameStatusRequest = await responseToSecondGetGameStatusRequest.Content.ReadAsStringAsync();
            var secondGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToSecondGetGameStatusRequest);

            responseToSecondGetGameStatusRequest.EnsureSuccessStatusCode();

            responseToFirstGetGameStatusRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            responseToSecondGetGameStatusRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            responseToFeedDragon.StatusCode.Should().Be(HttpStatusCode.OK);
            feedDragonResponse.Success.Should().BeTrue();
            firstGameStatus.DragonId.Should().Be(secondGameStatus.DragonId);
            firstGameStatus.Name.Should().Be(secondGameStatus.Name);
            firstGameStatus.AgeGroup.Should().Be(secondGameStatus.AgeGroup);
            firstGameStatus.Age.Should().BeLessThanOrEqualTo(secondGameStatus.Age);
            firstGameStatus.Happiness.Should().BeGreaterOrEqualTo(secondGameStatus.Happiness);
            firstGameStatus.Feedometer.Should().BeLessThan(secondGameStatus.Feedometer);
        }

        [Fact]
        public async Task ReturnFeedingFailureAfterFeedDragonRequestWhenMaxFeedometerReached()
        {
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon5";

            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);

            var responseToFirstGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToFirstGetGameStatusRequest = await responseToFirstGetGameStatusRequest.Content.ReadAsStringAsync();
            var firstGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToFirstGetGameStatusRequest);

            responseToFirstGetGameStatusRequest.EnsureSuccessStatusCode();

            var responseToFirstFeedDragonRequest = await client.PutAsync($"TamagotchiApi/feed/{dragonId}", new StringContent(""));
            var contentOfFirstResponseToFeedDragon = await responseToFirstFeedDragonRequest.Content.ReadAsStringAsync();
            var firstFeedDragonResponse = JsonConvert.DeserializeObject<FeedDragonResponse>(contentOfFirstResponseToFeedDragon);

            responseToFirstFeedDragonRequest.EnsureSuccessStatusCode();

            var responseToSecondFeedDragonRequest = await client.PutAsync($"TamagotchiApi/feed/{dragonId}", new StringContent(""));
            var contentOfSecondResponseToFeedDragon = await responseToSecondFeedDragonRequest.Content.ReadAsStringAsync();
            var secondFeedDragonResponse = JsonConvert.DeserializeObject<FeedDragonResponse>(contentOfFirstResponseToFeedDragon);

            responseToSecondFeedDragonRequest.EnsureSuccessStatusCode();

            var responseToSecondGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToSecondGetGameStatusRequest = await responseToSecondGetGameStatusRequest.Content.ReadAsStringAsync();
            var secondGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToSecondGetGameStatusRequest);

            responseToSecondGetGameStatusRequest.EnsureSuccessStatusCode();

            responseToFirstGetGameStatusRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            responseToSecondGetGameStatusRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            responseToFirstFeedDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            responseToSecondFeedDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            firstFeedDragonResponse.Success.Should().BeTrue();
            secondFeedDragonResponse.Success.Should().BeFalse();
            secondFeedDragonResponse.Reason.Should().Be(FeedingFailureReason.Full);
            firstGameStatus.DragonId.Should().Be(secondGameStatus.DragonId);
            firstGameStatus.Name.Should().Be(secondGameStatus.Name);
            firstGameStatus.AgeGroup.Should().Be(secondGameStatus.AgeGroup);
            firstGameStatus.Age.Should().BeLessThanOrEqualTo(secondGameStatus.Age);
            firstGameStatus.Happiness.Should().BeGreaterOrEqualTo(secondGameStatus.Happiness);
            firstGameStatus.Feedometer.Should().BeLessThan(secondGameStatus.Feedometer);
        }
    }
}