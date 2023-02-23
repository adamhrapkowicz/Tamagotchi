using Microsoft.Extensions.Options;
using Moq;
using Tamagotchi;

namespace TamagotchiTests
{
    public class LifeCycleManagerShould
    {
        private readonly ILifeCycleManager _lifeCycleManager;
        private readonly GameSettings _gameSettings;

        public LifeCycleManagerShould()
        {
            _gameSettings = GetMockSettings();
            var mockSettings = new Mock<IOptions<GameSettings>>();
            mockSettings.Setup(x => x.Value).Returns(_gameSettings);

            _lifeCycleManager = new LifeCycleManager(mockSettings.Object);
        }

        private static GameSettings GetMockSettings()
        {
            return new GameSettings
            {
                InitialFeedometer = 10,
                InitialHappiness = 10,
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
        public void CreateDragonWithCorrectNameOnCreateDragonCall()
        {
            //Arrange
            string name = "testdragon";

            //Act
            var dragonId = _lifeCycleManager.CreateDragon(name);

            //Assert
            Assert.IsType<Guid>(dragonId);
            Assert.True(Guid.Empty != dragonId);

            var dragon = _lifeCycleManager.GetDragonById(dragonId);
            Assert.NotNull(dragon);
            Assert.Equal(name, dragon.Name);
        }

        [Theory]
        [InlineData("TestDragon1")]
        [InlineData("TestDragon2")]
        [InlineData("TestDragon3")]
        public void ReturnCorrectDragonOnGetDragonByIdCall(string dragonName)
        {
            //Arrange
            var name = dragonName;
            var expectedAgeGroup = AgeGroup.Baby;
            var expectedAge = 0;

            //Act
            var testDragonId = _lifeCycleManager.CreateDragon(name);
            var returnedDragon = _lifeCycleManager.GetDragonById(testDragonId);

            //Assert
            Assert.IsType<Dragon>(returnedDragon);
            Assert.NotNull(returnedDragon);
            Assert.Equal(name, returnedDragon.Name);
            Assert.Equal(expectedAgeGroup, returnedDragon.AgeGroup);
            Assert.Equal(expectedAge, returnedDragon.Age);
            Assert.Equal(_gameSettings.InitialFeedometer, returnedDragon.Feedometer);
            Assert.Equal(_gameSettings.InitialHappiness, returnedDragon.Happiness);
            Assert.Equal(testDragonId, returnedDragon.DragonId);
            Assert.True(returnedDragon.IsAlive);
        }

        [Fact]
        public void ReturnSuccessResponseOnIncreaseFeedometerCallIfMaxFeedometerNotReached()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon4");

            //Act
            var returnedResponse = _lifeCycleManager.IncreaseFeedometer(testDragonId);

            //Assert
            Assert.NotNull(returnedResponse);
            Assert.IsType<FeedDragonResponse>(returnedResponse);
            Assert.True(returnedResponse.Success);
        }

        [Fact]
        public void ReturnDeadResponseOnIncreaseFeedometerCallIfDragonNotAlive()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon5");
            var testDragon5 = _lifeCycleManager.GetDragonById(testDragonId);
            testDragon5.IsAlive = false;
            var expectedReason = FeedingFailureReason.Dead;

            //Act
            var returnedResponse = _lifeCycleManager.IncreaseFeedometer(testDragonId);

            //Assert
            Assert.NotNull(returnedResponse);
            Assert.IsType<FeedDragonResponse>(returnedResponse);
            Assert.False(returnedResponse.Success);
            Assert.Equal(expectedReason, returnedResponse.Reason.GetValueOrDefault());
        }

        [Fact]
        public void ReturnFullResponseOnIncreaseFeedometerCallIfFeedometerMoreThanMax()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon6");
            var testDragon6 = _lifeCycleManager.GetDragonById(testDragonId);
            testDragon6.Feedometer = _lifeCycleManager.SetCareLevelsForAgeGroups(testDragon6).MaxFeedometerForAgeGroup + 1;
            var expectedReason = FeedingFailureReason.Full;

            //Act
            var returnedResponse = _lifeCycleManager.IncreaseFeedometer(testDragonId);

            //Assert
            Assert.NotNull(returnedResponse);
            Assert.IsType<FeedDragonResponse>(returnedResponse);
            Assert.False(returnedResponse.Success);
            Assert.Equal(expectedReason, returnedResponse.Reason.GetValueOrDefault());
        }
    }
}