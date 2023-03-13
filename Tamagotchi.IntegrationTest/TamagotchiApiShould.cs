using System.Net;
using FluentAssertions;
using Newtonsoft.Json;

namespace Tamagotchi.IntegrationTest
{
    public class TamagotchiApiShould
    {
        [Fact]
        public async Task StartGameWhenNameIsProvided()
        {
            // Arrange
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon1";

            // Act
            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);

            responseToStartGame.EnsureSuccessStatusCode();

            // Assert
            responseToStartGame.StatusCode.Should().Be(HttpStatusCode.OK);
            dragonId.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ReturnGameStatusWhenDragonIdIsProvided()
        {
            // Arrange
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon2";
            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);
            
            // Act
            var responseToGetGameStatus = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToGetGameStatus = await responseToGetGameStatus.Content.ReadAsStringAsync();
            var gameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToGetGameStatus);

            responseToGetGameStatus.EnsureSuccessStatusCode();

            // Assert
            responseToGetGameStatus.StatusCode.Should().Be(HttpStatusCode.OK);
            gameStatus.Should().NotBeNull();
            gameStatus.Name.Should().Be(dragonName);
            gameStatus.AgeGroup.Should().Be(AgeGroup.Baby);
            gameStatus.DragonId.Should().Be(dragonId);
        }

        [Fact]
        public async Task ReturnDifferentGameStatusAfterTimePasses()
        {
            // Arrange
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon3";
            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);
            
            // Act
            Thread.Sleep(1000);

            var responseToFirstGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToFirstGetGameStatusRequest = await responseToFirstGetGameStatusRequest.Content.
                ReadAsStringAsync();
            var firstGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToFirstGetGameStatusRequest);

            responseToFirstGetGameStatusRequest.EnsureSuccessStatusCode();

            Thread.Sleep(10000);

            var responseToSecondGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToSecondGetGameStatusRequest = await responseToSecondGetGameStatusRequest.Content.
                ReadAsStringAsync();
            var secondGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToSecondGetGameStatusRequest);

            responseToSecondGetGameStatusRequest.EnsureSuccessStatusCode();

            // Assert
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
            // Arrange
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon4";

            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);

            // Act
            var responseToFirstGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToFirstGetGameStatusRequest = await responseToFirstGetGameStatusRequest.Content.
                ReadAsStringAsync();
            var firstGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToFirstGetGameStatusRequest);

            responseToFirstGetGameStatusRequest.EnsureSuccessStatusCode();
            
            var responseToFeedDragon = await client.PutAsync($"TamagotchiApi/feed/{dragonId}", new StringContent(""));
            var contentOfTheResponseToFeedDragon = await responseToFeedDragon.Content.ReadAsStringAsync();
            var feedDragonResponse = JsonConvert.DeserializeObject<FeedDragonResponse>(contentOfTheResponseToFeedDragon);

            responseToFeedDragon.EnsureSuccessStatusCode();

            var responseToSecondGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToSecondGetGameStatusRequest = await responseToSecondGetGameStatusRequest.Content.
                ReadAsStringAsync();
            var secondGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToSecondGetGameStatusRequest);

            responseToSecondGetGameStatusRequest.EnsureSuccessStatusCode();


            // Assert
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
            // Arrange
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon5";

            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);

            // Act
            var responseToFirstFeedDragonRequest = await client.PutAsync($"TamagotchiApi/feed/{dragonId}",
                new StringContent(""));
            var contentOfFirstResponseToFeedDragon = await responseToFirstFeedDragonRequest.Content.ReadAsStringAsync();
            var firstFeedDragonResponse = JsonConvert.DeserializeObject
                <FeedDragonResponse>(contentOfFirstResponseToFeedDragon);

            var responseToSecondFeedDragonRequest = await client.PutAsync($"TamagotchiApi/feed/{dragonId}",
                new StringContent(""));
            var contentOfSecondResponseToFeedDragon = await responseToSecondFeedDragonRequest.Content.
                ReadAsStringAsync();
            var secondFeedDragonResponse = JsonConvert.DeserializeObject
                <FeedDragonResponse>(contentOfSecondResponseToFeedDragon);

            // Assert
            responseToFirstFeedDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            responseToSecondFeedDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            firstFeedDragonResponse.Success.Should().BeTrue();
            secondFeedDragonResponse.Success.Should().BeFalse();
            secondFeedDragonResponse.Reason.Should().Be(FeedingFailureReason.Full);
        }

        [Fact]
        public async Task ReturnFeedingFailureAfterPetDragonRequestWhenDragonIsNotAlive()
        {
            // Arrange
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon6";

            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);

            var dragonIsAlive = true;

            while (dragonIsAlive)
            {
                var responseToGetGameStatusRequest = await client.GetAsync($"/TamagotchiApi/{dragonId}");
                var contentOfTheResponseToGetGameStatusRequest = await responseToGetGameStatusRequest.Content.
                    ReadAsStringAsync();
                var gameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfTheResponseToGetGameStatusRequest);
                if (gameStatus.IsAlive != true)
                    dragonIsAlive = false;
            }

            // Act
            var responseToFeedDragonRequest = await client.PutAsync($"TamagotchiApi/feed/{dragonId}",
                new StringContent(""));
            var contentOfResponseToFeedDragon = await responseToFeedDragonRequest.Content.ReadAsStringAsync();
            var feedDragonResponse = JsonConvert.DeserializeObject<FeedDragonResponse>(contentOfResponseToFeedDragon);

            //Assert
            responseToFeedDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            feedDragonResponse.Success.Should().BeFalse();
            feedDragonResponse.Reason.Should().Be(FeedingFailureReason.Dead);
        }

        [Fact]
        public async Task ReturnIncreasedHappinessAfterPetDragonRequest()
        {
            // Arrange
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon7";

            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);

            // Act
            var responseToFirstGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToFirstGetGameStatusRequest = await responseToFirstGetGameStatusRequest.Content.
                ReadAsStringAsync();
            var firstGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToFirstGetGameStatusRequest);

            responseToFirstGetGameStatusRequest.EnsureSuccessStatusCode();

            var responseToPetDragon = await client.PutAsync($"TamagotchiApi/pet/{dragonId}", new StringContent(""));
            var contentOfTheResponseToPetDragon = await responseToPetDragon.Content.ReadAsStringAsync();
            var petDragonResponse = JsonConvert.DeserializeObject<FeedDragonResponse>(contentOfTheResponseToPetDragon);

            responseToPetDragon.EnsureSuccessStatusCode();

            var responseToSecondGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
            var contentOfResponseToSecondGetGameStatusRequest = await responseToSecondGetGameStatusRequest.Content.
                ReadAsStringAsync();
            var secondGameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfResponseToSecondGetGameStatusRequest);

            responseToSecondGetGameStatusRequest.EnsureSuccessStatusCode();


            // Assert
            responseToFirstGetGameStatusRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            responseToSecondGetGameStatusRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            responseToPetDragon.StatusCode.Should().Be(HttpStatusCode.OK);
            petDragonResponse.Success.Should().BeTrue();
            firstGameStatus.DragonId.Should().Be(secondGameStatus.DragonId);
            firstGameStatus.Name.Should().Be(secondGameStatus.Name);
            firstGameStatus.AgeGroup.Should().Be(secondGameStatus.AgeGroup);
            firstGameStatus.Age.Should().BeLessThanOrEqualTo(secondGameStatus.Age);
            firstGameStatus.Happiness.Should().BeLessThan(secondGameStatus.Happiness);
            firstGameStatus.Feedometer.Should().BeGreaterOrEqualTo(secondGameStatus.Feedometer);
        }

        [Fact]
        public async Task ReturnPettingFailureAfterPetDragonRequestWhenMaxHappinessReached()
        {
            // Arrange
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon8";

            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);

            // Act
            var responseToFirstPetDragonRequest = await client.PutAsync($"TamagotchiApi/pet/{dragonId}",
                new StringContent(""));
            var contentOfFirstResponseToPetDragon = await responseToFirstPetDragonRequest.Content.ReadAsStringAsync();
            var firstPetDragonResponse = JsonConvert.DeserializeObject<PetDragonResponse>(contentOfFirstResponseToPetDragon);

            var responseToSecondPetDragonRequest = await client.PutAsync($"TamagotchiApi/pet/{dragonId}",
                new StringContent(""));
            var contentOfSecondResponseToPetDragon = await responseToSecondPetDragonRequest.Content.ReadAsStringAsync();
            var secondPetDragonResponse = JsonConvert.DeserializeObject<PetDragonResponse>(contentOfSecondResponseToPetDragon);

            // Assert
            responseToFirstPetDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            responseToSecondPetDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            firstPetDragonResponse.Success.Should().BeTrue();
            secondPetDragonResponse.Success.Should().BeFalse();
            secondPetDragonResponse.Reason.Should().Be(PettingFailureReason.Overpetted);
        }

        [Fact]
        public async Task ReturnPettingFailureAfterPetDragonRequestWhenDragonIsNotAlive()
        {
            // Arrange
            using var client = new TestClientProvider().Client;

            const string dragonName = "myTestDragon9";

            var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

            var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
            var dragonId = JsonConvert.DeserializeObject<Guid>(contentOfResponseToStartGame);

            var dragonIsAlive = true;

            while (dragonIsAlive)
            {
                var responseToGetGameStatusRequest = await client.GetAsync($"/TamagotchiApi/{dragonId}");
                var contentOfTheResponseToGetGameStatusRequest = await responseToGetGameStatusRequest.Content.
                    ReadAsStringAsync();
                var gameStatus = JsonConvert.DeserializeObject<Dragon>(contentOfTheResponseToGetGameStatusRequest);
                if (gameStatus.IsAlive != true)
                    dragonIsAlive = false;
            }

            // Act
            var responseToPetDragonRequest = await client.PutAsync($"TamagotchiApi/pet/{dragonId}",
                new StringContent(""));
            var contentOfResponseToPetDragon = await responseToPetDragonRequest.Content.ReadAsStringAsync();
            var petDragonResponse = JsonConvert.DeserializeObject<PetDragonResponse>(contentOfResponseToPetDragon);

            //Assert
            responseToPetDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            petDragonResponse.Success.Should().BeFalse();
            petDragonResponse.Reason.Should().Be(PettingFailureReason.Dead);
        }
    }
}