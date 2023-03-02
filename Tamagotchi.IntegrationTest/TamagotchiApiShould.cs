using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using Tamagotchi.Controllers;

namespace Tamagotchi.IntegrationTest
{
    public class TamagotchiApiShould
    {
        private readonly ILifeCycleManager _lifeCycleManager;
        private readonly GameSettings _gameSettings;

        public TamagotchiApiShould(ILifeCycleManager lifeCycleManager, GameSettings gameSettings)
        {
            _gameSettings = GetMockGameSettings();
            var mockGameSettings = new Mock<IOptions<GameSettings>>();
            mockGameSettings.Setup(x => x.Value).Returns(_gameSettings);

            _lifeCycleManager = new LifeCycleManager(mockGameSettings.Object);
        }

        private static GameSettings GetMockGameSettings()
        {
            return new GameSettings
            {
                LifeProgressTimerInterval = 700,
                InitialFeedometer = 10,
                InitialHappiness = 10,
                AgeIncrement = 0.1,
                MinValueOfFeedometer = 0,
                MinValueOfHappiness = 0,
                MaxAge = 99.00,
                NameNeglectPenalty = 2,
                BabySettings = new AgeGroupSettings
                {
                    FeedometerIncrement = 10,
                    HappinessIncrement = 15,
                    HungerIncrement = 3,
                    SadnessIncrement = 4,
                    MaxFeedometerForAgeGroup = 30,
                    MaxHappinessForAgeGroup = 200
                },
                ChildSettings = new AgeGroupSettings
                {
                    FeedometerIncrement = 10,
                    HappinessIncrement = 15,
                    HungerIncrement = 3,
                    SadnessIncrement = 4,
                    MaxFeedometerForAgeGroup = 30,
                    MaxHappinessForAgeGroup = 200
                },
                TeenSettings = new AgeGroupSettings
                {
                    FeedometerIncrement = 10,
                    HappinessIncrement = 15,
                    HungerIncrement = 3,
                    SadnessIncrement = 4,
                    MaxFeedometerForAgeGroup = 30,
                    MaxHappinessForAgeGroup = 200
                },
                AdultSettings = new AgeGroupSettings
                {
                    FeedometerIncrement = 10,
                    HappinessIncrement = 15,
                    HungerIncrement = 3,
                    SadnessIncrement = 4,
                    MaxFeedometerForAgeGroup = 30,
                    MaxHappinessForAgeGroup = 200
                },
                SeniorSettings = new AgeGroupSettings
                {
                    FeedometerIncrement = 10,
                    HappinessIncrement = 15,
                    HungerIncrement = 3,
                    SadnessIncrement = 4,
                    MaxFeedometerForAgeGroup = 30,
                    MaxHappinessForAgeGroup = 200
                }
            };
        }

        [Fact]
        public async Task StartGameWhenNameIsProvided()
        {

            var client = new TestClientProvider().Client;

            var response = await client.PostAsync("/TamagotchiApi/{dragonName}", new StringContent("testDragon"));

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        //[Fact]
        //public async Task ReturnGameStatusWhenDragonIdIsProvided()
        //{
        //    var client = new TestClientProvider().Client;
        //    var dragonId = new TamagotchiApiController(_lifeCycleManager).StartGame("testDragon");

        //    var response = await client.GetAsync($"TamagotchiApi/{dragonId}");

        //    response.EnsureSuccessStatusCode();

        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //}
    }
}