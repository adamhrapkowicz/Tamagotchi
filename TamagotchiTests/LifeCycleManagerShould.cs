using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Moq.EntityFrameworkCore;
using Tamagotchi;
using Tamagotchi.Contracts;
using TamagotchiData.Models;

namespace TamagotchiUnitTests
{
    public class LifeCycleManagerShould
    {
        private readonly ILifeCycleManager _lifeCycleManager;
        private readonly GameSettings _gameSettings;
        //private readonly TamagotchiDbContext _tamagotchiDbContext;

        public LifeCycleManagerShould()
        {
            _gameSettings = GetMockSettings();
            var mockSettings = new Mock<IOptions<GameSettings>>();
            mockSettings.Setup(x => x.Value).Returns(_gameSettings);

            //_tamagotchiDbContext = GetMockDbSet();
            var mockContext = new Mock<TamagotchiDbContext>();
            mockContext.Setup<DbSet<Dragon>>(x => x.Dragons).ReturnsDbSet(GetMockDbSet());
            
            _lifeCycleManager = new LifeCycleManager(
                mockSettings.Object, mockContext.Object);
        }

        private static IEnumerable<Dragon> GetMockDbSet()
        {
            return new List<Dragon>();
        }
         

        private static GameSettings GetMockSettings()
        {
            return new GameSettings
            {
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
        public void CreateDragonWithCorrectNameOnCreateDragonCall()
        {
            //Arrange
            const string name = "testdragon";

            //Act
            var dragonId = _lifeCycleManager.CreateDragon(name);

            //Assert
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
            const AgeGroup expectedAgeGroup = AgeGroup.Baby;
            const int expectedAge = 0;

            //Act
            var testDragonId = _lifeCycleManager.CreateDragon(dragonName);
            var returnedDragon = _lifeCycleManager.GetDragonById(testDragonId);

            //Assert
            Assert.NotNull(returnedDragon);
            Assert.Equal(dragonName, returnedDragon.Name);
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
            var testDragon4 = _lifeCycleManager.GetDragonById(testDragonId);
            var startingFeedometer = testDragon4.Feedometer;

            //Act
            var returnedResponse = _lifeCycleManager.IncreaseFeedometer(testDragonId);

            //Assert
            Assert.NotNull(returnedResponse);
            Assert.True(returnedResponse.Success);
            Assert.True(startingFeedometer < testDragon4.Feedometer);
        }

        [Fact]
        public void ReturnDeadResponseOnIncreaseFeedometerCallIfDragonNotAlive()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon5");
            var testDragon5 = _lifeCycleManager.GetDragonById(testDragonId);
            testDragon5.IsAlive = false;
            const FeedingFailureReason expectedReason = FeedingFailureReason.Dead;
            var startingFeedometer = testDragon5.Feedometer;

            //Act
            var returnedResponse = _lifeCycleManager.IncreaseFeedometer(testDragonId);

            //Assert
            Assert.NotNull(returnedResponse);
            Assert.False(returnedResponse.Success);
            Assert.Equal(expectedReason, returnedResponse.Reason.GetValueOrDefault());
            Assert.True(startingFeedometer == testDragon5.Feedometer);
        }

        [Fact]
        public void ReturnFullResponseOnIncreaseFeedometerCallIfFeedometerMoreThanMax()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon6");
            var testDragon6 = _lifeCycleManager.GetDragonById(testDragonId);
            testDragon6.Feedometer = _gameSettings.BabySettings.MaxFeedometerForAgeGroup;
            const FeedingFailureReason expectedReason = FeedingFailureReason.Full;
            var startingFeedometer = testDragon6.Feedometer;

            //Act
            var returnedResponse = _lifeCycleManager.IncreaseFeedometer(testDragonId);

            //Assert
            Assert.NotNull(returnedResponse);
            Assert.False(returnedResponse.Success);
            Assert.Equal(expectedReason, returnedResponse.Reason.GetValueOrDefault());
            Assert.True(startingFeedometer == testDragon6.Feedometer);
        }

        [Fact]
        public void ReturnSuccessResponseOnIncreaseHappinessCallIfMaxHappinessNotReached()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon4");
            var testDragon4 = _lifeCycleManager.GetDragonById(testDragonId);
            var startingHappiness = testDragon4.Happiness;

            //Act
            var returnedResponse = _lifeCycleManager.IncreaseHappiness(testDragonId);

            //Assert
            returnedResponse.Should().NotBeNull();
            returnedResponse.Success.Should().BeTrue();
            testDragon4.Happiness.Should().BeGreaterThan(startingHappiness);
        }

        [Fact]
        public void ReturnDeadResponseOnIncreaseHappinessCallIfDragonNotAlive()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon5");
            var testDragon5 = _lifeCycleManager.GetDragonById(testDragonId);
            testDragon5.IsAlive = false;
            const PettingFailureReason expectedReason = PettingFailureReason.Dead;
            var startingHappiness = testDragon5.Happiness;

            //Act
            var returnedResponse = _lifeCycleManager.IncreaseHappiness(testDragonId);

            //Assert
            returnedResponse.Should().NotBeNull();
            returnedResponse.Success.Should().BeFalse();
            returnedResponse.Reason.Should().Be(expectedReason);
            testDragon5.Happiness.Should().Be(startingHappiness);
        }

        [Fact]
        public void ReturnOverpettedResponseOnIncreaseFeedometerCallIfFeedometerMoreThanMax()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon6");
            var testDragon6 = _lifeCycleManager.GetDragonById(testDragonId);
            testDragon6.Happiness = _gameSettings.BabySettings.MaxHappinessForAgeGroup;
            const PettingFailureReason expectedReason = PettingFailureReason.Overpetted;
            var startingHappiness = testDragon6.Happiness;

            //Act
            var returnedResponse = _lifeCycleManager.IncreaseHappiness(testDragonId);

            //Assert
            returnedResponse.Should().NotBeNull();
            returnedResponse.Success.Should().BeFalse();
            returnedResponse.Reason.Should().Be(expectedReason);
            testDragon6.Happiness.Should().Be(startingHappiness);
        }

        //[Fact]
        //public void ProgressesLifeOnProgressLifeCall()
        //{
        //    //Arrange
        //    var testDragonId = _lifeCycleManager.CreateDragon("TestDragon");
        //    var testDragon = _lifeCycleManager.GetDragonById(testDragonId);
        //    var startingAge = testDragon.Age;
        //    var startingHappiness = testDragon.Happiness;
        //    var startingFeedometer = testDragon.Feedometer;

        //    //Act
        //    _lifeCycleManager.ProgressLife();

        //    //Assert
        //    testDragon.Age.Should().BeGreaterThan(startingAge);
        //    testDragon.Happiness.Should().BeLessThan(startingHappiness);
        //    testDragon.Feedometer.Should().BeLessThan(startingFeedometer);
        //}

        //[Fact]
        //public void ChangeDragonIsAliveToFalseOnProgressLifeCallIfMinFeedometerReached()
        //{
        //    //Arrange
        //    var testDragonId = _lifeCycleManager.CreateDragon("TestDragon");
        //    var testDragon = _lifeCycleManager.GetDragonById(testDragonId);
        //    var startingAge = testDragon.Age;
        //    var startingHappiness = testDragon.Happiness;
        //    var startingFeedometer = testDragon.Feedometer;
        //    testDragon.Feedometer = _gameSettings.MinValueOfFeedometer;


        //    //Act
        //    _lifeCycleManager.ProgressLife();

        //    //Assert
        //    testDragon.Age.Should().BeGreaterThan(startingAge);
        //    testDragon.Happiness.Should().BeLessThan(startingHappiness);
        //    testDragon.Feedometer.Should().BeLessThan(startingFeedometer);
        //    testDragon.IsAlive.Should().BeFalse();
        //}

        //[Fact]
        //public void ChangeDragonIsAliveToFalseOnProgressLifeCallIfMinHappinessReached()
        //{
        //    //Arrange
        //    var testDragonId = _lifeCycleManager.CreateDragon("TestDragon");
        //    var testDragon = _lifeCycleManager.GetDragonById(testDragonId);
        //    var startingAge = testDragon.Age;
        //    var startingHappiness = testDragon.Happiness;
        //    var startingFeedometer = testDragon.Feedometer;
        //    testDragon.Happiness = _gameSettings.MinValueOfHappiness;

        //    //Act
        //    _lifeCycleManager.ProgressLife();

        //    //Assert
        //    testDragon.Age.Should().BeGreaterThan(startingAge);
        //    testDragon.Happiness.Should().BeLessThan(startingHappiness);
        //    testDragon.Feedometer.Should().BeLessThan(startingFeedometer);
        //    testDragon.IsAlive.Should().BeFalse();
        //}

        //[Fact]
        //public void ChangeDragonIsAliveToFalseOnProgressLifeCallIfMaxAgeReached()
        //{
        //    //Arrange
        //    var testDragonId = _lifeCycleManager.CreateDragon("TestDragon");
        //    var testDragon = _lifeCycleManager.GetDragonById(testDragonId);
        //    var startingAge = testDragon.Age;
        //    var startingHappiness = testDragon.Happiness;
        //    var startingFeedometer = testDragon.Feedometer;
        //    testDragon.Age = _gameSettings.MaxAge;

        //    //Act
        //    _lifeCycleManager.ProgressLife();

        //    //Assert
        //    testDragon.Age.Should().BeGreaterThan(startingAge);
        //    testDragon.Happiness.Should().BeLessThan(startingHappiness);
        //    testDragon.Feedometer.Should().BeLessThan(startingFeedometer);
        //    testDragon.IsAlive.Should().BeFalse();
        //}

        //[Fact]
        //public void NotProgressLifeOnProgressLifeCallIfDragonIsNotAlive()
        //{
        //    //Arrange
        //    var testDragonId = _lifeCycleManager.CreateDragon("TestDragon1");
        //    var testDragon = _lifeCycleManager.GetDragonById(testDragonId);
        //    var startingAge = testDragon.Age;
        //    var startingHappiness = testDragon.Happiness;
        //    var startingFeedometer = testDragon.Feedometer;
        //    testDragon.IsAlive = false;

        //    //Act
        //    _lifeCycleManager.ProgressLife();

        //    //Assert
        //    testDragon.Age.Should().Be(startingAge);
        //    testDragon.Happiness.Should().Be(startingHappiness);
        //    testDragon.Feedometer.Should().Be(startingFeedometer);
        //}
    }
}