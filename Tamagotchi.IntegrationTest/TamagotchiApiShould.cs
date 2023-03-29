using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Tamagotchi.Contracts;
using TamagotchiData.Models;

namespace Tamagotchi.IntegrationTest;

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
        var dragonId = JsonConvert.DeserializeObject<StartGameResponse>(contentOfResponseToStartGame);

        // Assert
        responseToStartGame.StatusCode.Should().Be(HttpStatusCode.Created);
        dragonId.Should().NotBeNull();
    }

    [Fact]
    public async Task ReturnFailureStartGameResponseWhenNameIsNotProvided()
    {
        // Arrange
        using var client = new TestClientProvider().Client;

        const string dragonName = "";

        // Act
        var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

        var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
        var dragonId = JsonConvert.DeserializeObject<StartGameResponse>(contentOfResponseToStartGame);

        // Assert
        responseToStartGame.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        dragonId.DragonId.Should().BeEmpty();
    }

    [Fact]
    public async Task ReturnGameStatusResponseWhenDragonIdIsProvided()
    {
        // Arrange
        using var client = new TestClientProvider().Client;

        var dragonId = await GetDragonId(client);

        // Act
        var responseToGetGameStatus = await client.GetAsync($"TamagotchiApi/{dragonId}");
        var contentOfResponseToGetGameStatus = await responseToGetGameStatus.Content.ReadAsStringAsync();
        var gameStatus = JsonConvert.DeserializeObject<GameStatusResponse>(contentOfResponseToGetGameStatus);

        responseToGetGameStatus.EnsureSuccessStatusCode();

        // Assert
        responseToGetGameStatus.StatusCode.Should().Be(HttpStatusCode.OK);
        gameStatus.Should().NotBeNull();
        gameStatus.AgeGroup.Should().Be(AgeGroup.Baby);
    }

    [Fact]
    public async Task ReturnDifferentGameStatusResponseContentAfterTimePasses()
    {
        // Arrange
        using var client = new TestClientProvider().Client;

        var dragonId = await GetDragonId(client);

        // Act

        var responseToFirstGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
        var contentOfResponseToFirstGetGameStatusRequest =
            await responseToFirstGetGameStatusRequest.Content.ReadAsStringAsync();
        var firstGameStatus =
            JsonConvert.DeserializeObject<GameStatusResponse>(contentOfResponseToFirstGetGameStatusRequest);

        responseToFirstGetGameStatusRequest.EnsureSuccessStatusCode();

        Thread.Sleep(10000);

        var responseToSecondGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
        var contentOfResponseToSecondGetGameStatusRequest =
            await responseToSecondGetGameStatusRequest.Content.ReadAsStringAsync();
        var secondGameStatus =
            JsonConvert.DeserializeObject<GameStatusResponse>(contentOfResponseToSecondGetGameStatusRequest);

        responseToSecondGetGameStatusRequest.EnsureSuccessStatusCode();

        // Assert
        firstGameStatus.Should().NotBeNull();
        secondGameStatus.Should().NotBeNull();
        firstGameStatus.IsAlive.Should().BeTrue();
        secondGameStatus.Name.Should().Be(firstGameStatus.Name);
        secondGameStatus.Age.Should().BeGreaterThan(firstGameStatus.Age);
        secondGameStatus.Happiness.Should().BeLessThan(firstGameStatus.Happiness);
        secondGameStatus.Feedometer.Should().BeLessThan(firstGameStatus.Feedometer);
    }

    [Fact]
    public async Task ReturnGetGameStatusResponseFailureReasonAsDeadWhenDragonIsNotAlive()
    {
        // Arrange
        using var client = new TestClientProvider().Client;

        var dragonId = await GetDragonId(client);
        await WaitForDragonToDie(client, dragonId);

        // Act
        var responseToGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
        var contentOfResponseToGetGameStatus = await responseToGetGameStatusRequest.Content.ReadAsStringAsync();
        var gameStatus = JsonConvert.DeserializeObject<GameStatusResponse>(contentOfResponseToGetGameStatus);

        // Assert
        responseToGetGameStatusRequest.StatusCode.Should().Be(HttpStatusCode.OK);
        gameStatus.Success.Should().BeFalse();
        gameStatus.Reason.Should().Be(GetGameStatusFailureReason.Dead);
        gameStatus.IsAlive.Should().BeFalse();
    }

    [Fact]
    public async Task ReturnIncreasedFeedometerAfterFeedDragonRequest()
    {
        // Arrange
        using var client = new TestClientProvider().Client;

        var dragonId = await GetDragonId(client);

        // Act
        var responseToFirstGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
        var contentOfResponseToFirstGetGameStatusRequest =
            await responseToFirstGetGameStatusRequest.Content.ReadAsStringAsync();
        var firstGameStatus =
            JsonConvert.DeserializeObject<GameStatusResponse>(contentOfResponseToFirstGetGameStatusRequest);

        responseToFirstGetGameStatusRequest.EnsureSuccessStatusCode();

        var responseToFeedDragon = await client.PutAsync($"TamagotchiApi/feed/{dragonId}", new StringContent(""));
        var contentOfTheResponseToFeedDragon = await responseToFeedDragon.Content.ReadAsStringAsync();
        var feedDragonResponse = JsonConvert.DeserializeObject<FeedDragonResponse>(contentOfTheResponseToFeedDragon);

        responseToFeedDragon.EnsureSuccessStatusCode();

        var responseToSecondGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
        var contentOfResponseToSecondGetGameStatusRequest =
            await responseToSecondGetGameStatusRequest.Content.ReadAsStringAsync();
        var secondGameStatus =
            JsonConvert.DeserializeObject<GameStatusResponse>(contentOfResponseToSecondGetGameStatusRequest);

        responseToSecondGetGameStatusRequest.EnsureSuccessStatusCode();


        // Assert
        responseToFeedDragon.StatusCode.Should().Be(HttpStatusCode.OK);
        feedDragonResponse.Success.Should().BeTrue();
        secondGameStatus.Feedometer.Should().BeGreaterThan(firstGameStatus.Feedometer);
    }

    [Fact]
    public async Task ReturnFeedingFailureAfterFeedDragonRequestWhenMaxFeedometerReached()
    {
        // Arrange
        using var client = new TestClientProvider().Client;

        var dragonId = await GetDragonId(client);

        var responseToFirstFeedDragonRequest = await client.PutAsync($"TamagotchiApi/feed/{dragonId}",
            new StringContent(""));

        // Act
        var responseToSecondFeedDragonRequest = await client.PutAsync($"TamagotchiApi/feed/{dragonId}",
            new StringContent(""));
        var contentOfSecondResponseToFeedDragon = await responseToSecondFeedDragonRequest.Content.ReadAsStringAsync();
        var secondFeedDragonResponse = JsonConvert.DeserializeObject
            <FeedDragonResponse>(contentOfSecondResponseToFeedDragon);

        // Assert
        responseToFirstFeedDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
        responseToSecondFeedDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
        secondFeedDragonResponse.Success.Should().BeFalse();
        secondFeedDragonResponse.Reason.Should().Be(FeedingFailureReason.Full);
    }

    [Fact]
    public async Task ReturnFeedingFailureAfterPetDragonRequestWhenDragonIsNotAlive()
    {
        // Arrange
        using var client = new TestClientProvider().Client;

        var dragonId = await GetDragonId(client);

        await WaitForDragonToDie(client, dragonId);

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

        var dragonId = await GetDragonId(client);

        var responseToFirstGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
        var contentOfResponseToFirstGetGameStatusRequest =
            await responseToFirstGetGameStatusRequest.Content.ReadAsStringAsync();
        var firstGameStatus =
            JsonConvert.DeserializeObject<GameStatusResponse>(contentOfResponseToFirstGetGameStatusRequest);

        responseToFirstGetGameStatusRequest.EnsureSuccessStatusCode();

        // Act
        var responseToPetDragon = await client.PutAsync($"TamagotchiApi/pet/{dragonId}", new StringContent(""));
        var contentOfTheResponseToPetDragon = await responseToPetDragon.Content.ReadAsStringAsync();
        var petDragonResponse = JsonConvert.DeserializeObject<FeedDragonResponse>(contentOfTheResponseToPetDragon);

        responseToPetDragon.EnsureSuccessStatusCode();

        var responseToSecondGetGameStatusRequest = await client.GetAsync($"TamagotchiApi/{dragonId}");
        var contentOfResponseToSecondGetGameStatusRequest =
            await responseToSecondGetGameStatusRequest.Content.ReadAsStringAsync();
        var secondGameStatus =
            JsonConvert.DeserializeObject<GameStatusResponse>(contentOfResponseToSecondGetGameStatusRequest);

        responseToSecondGetGameStatusRequest.EnsureSuccessStatusCode();


        // Assert
        responseToPetDragon.StatusCode.Should().Be(HttpStatusCode.OK);
        petDragonResponse.Success.Should().BeTrue();
        secondGameStatus.Happiness.Should().BeGreaterThan(firstGameStatus.Happiness);
    }

    [Fact]
    public async Task ReturnPettingFailureAfterPetDragonRequestWhenMaxHappinessReached()
    {
        // Arrange
        using var client = new TestClientProvider().Client;

        var dragonId = await GetDragonId(client);

        var responseToFirstPetDragonRequest = await client.PutAsync($"TamagotchiApi/pet/{dragonId}",
            new StringContent(""));

        // Act
        var responseToSecondPetDragonRequest = await client.PutAsync($"TamagotchiApi/pet/{dragonId}",
            new StringContent(""));
        var contentOfSecondResponseToPetDragon = await responseToSecondPetDragonRequest.Content.ReadAsStringAsync();
        var secondPetDragonResponse =
            JsonConvert.DeserializeObject<PetDragonResponse>(contentOfSecondResponseToPetDragon);

        // Assert
        responseToFirstPetDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
        responseToSecondPetDragonRequest.StatusCode.Should().Be(HttpStatusCode.OK);
        secondPetDragonResponse.Success.Should().BeFalse();
        secondPetDragonResponse.Reason.Should().Be(PettingFailureReason.Overpetted);
    }

    [Fact]
    public async Task ReturnPettingFailureAfterPetDragonRequestWhenDragonIsNotAlive()
    {
        // Arrange
        using var client = new TestClientProvider().Client;

        var dragonId = await GetDragonId(client);

        await WaitForDragonToDie(client, dragonId);

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

    private static async Task<Guid> GetDragonId(HttpClient client)
    {
        const string dragonName = "myTestDragon";

        var responseToStartGame = await client.PostAsync($"/TamagotchiApi/{dragonName}", new StringContent(""));

        var contentOfResponseToStartGame = await responseToStartGame.Content.ReadAsStringAsync();
        var valueOfCreateGameResponse = JsonConvert.DeserializeObject<StartGameResponse>(contentOfResponseToStartGame);

        return valueOfCreateGameResponse.DragonId;
    }

    private static async Task WaitForDragonToDie(HttpClient client, Guid dragonId)
    {
        var dragonIsAlive = true;

        while (dragonIsAlive)
        {
            var responseToGetGameStatusRequest = await client.GetAsync($"/TamagotchiApi/{dragonId}");
            var contentOfTheResponseToGetGameStatusRequest =
                await responseToGetGameStatusRequest.Content.ReadAsStringAsync();
            var gameStatus =
                JsonConvert.DeserializeObject<GameStatusResponse>(contentOfTheResponseToGetGameStatusRequest);
            dragonIsAlive = gameStatus.IsAlive;

            await Task.Delay(500);
        }
    }
}