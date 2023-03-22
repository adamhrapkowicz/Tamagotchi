using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Tamagotchi;
using Tamagotchi.Contracts;
using TamagotchiData.Models;

namespace TamagotchiUnitTests
{
    public class LifeCycleManagerShould
    {
        private readonly ILifeCycleManager _lifeCycleManager;
        private readonly GameSettings _gameSettings;
        private readonly ITamagotchiRepository _tamagotchiRepository;

        public LifeCycleManagerShould(ITamagotchiRepository tamagotchiRepository)
        {
            //_tamagotchiRepository = tamagotchiRepository;

            _gameSettings = GetMockSettings();
            var mockSettings = new Mock<IOptions<GameSettings>>();
            mockSettings.Setup(x => x.Value).Returns(_gameSettings);

            var builder = new DbContextOptionsBuilder<TamagotchiDbContext>();
            builder.UseInMemoryDatabase("Testing");
            var tamagotchiDbOptions = new TamagotchiDbContext(builder.Options);

            _tamagotchiRepository = new Mock<TamagotchiRepository>(tamagotchiDbOptions).Object;

            var mockRepository = new Mock<ITamagotchiRepository>();
            mockRepository.Setup(x => x).Returns(_tamagotchiRepository);
            //var tamagotchiDbContext = GetInMemoryTamagotchiDbContext();
            _lifeCycleManager = new LifeCycleManager(mockSettings.Object, _tamagotchiRepository);
        }

        //private static Mock<ITamagotchiRepository> GetMockRepository()
        //{
        //    return new TamagotchiRepository(_tamagotchi)
        //    {

        //    };
        //}

        //public static TamagotchiDbContext GetInMemoryTamagotchiDbContext()
        //{
        //    var builder = new DbContextOptionsBuilder<TamagotchiDbContext>();
        //    builder.UseInMemoryDatabase("Testing");
        //    var tamagotchiDbOptions = new TamagotchiDbContext(builder.Options);
        //    return tamagotchiDbOptions;
        //}

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
        public async Task CreateDragonWithCorrectNameOnCreateDragonCall()
        {
            //Arrange
            const string name = "testdragon";

            //Act
            var dragonId = _lifeCycleManager.CreateDragon(name);

            //Assert
            Assert.True(Guid.Empty != dragonId);

            var dragon = await _lifeCycleManager.GetDragonByIdAsync(dragonId);
            Assert.NotNull(dragon);
            Assert.Equal(name, dragon.Name);
        }

        [Theory]
        [InlineData("TestDragon1")]
        [InlineData("TestDragon2")]
        [InlineData("TestDragon3")]
        public async Task ReturnCorrectDragonOnGetDragonByIdCall(string dragonName)
        {
            //Arrange
            const AgeGroup expectedAgeGroup = AgeGroup.Baby;
            const int expectedAge = 0;

            //Act
            var testDragonId = _lifeCycleManager.CreateDragon(dragonName);
            var returnedDragon = await _lifeCycleManager.GetDragonByIdAsync(testDragonId);

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
        public async Task ReturnSuccessResponseOnIncreaseFeedometerCallIfMaxFeedometerNotReached()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon4");
            var testDragon4 = _lifeCycleManager.GetDragonByIdAsync(testDragonId).Result;
            var startingFeedometer = testDragon4!.Feedometer;

            //Act
            var returnedResponse = await _lifeCycleManager.IncreaseFeedometerAsync(testDragonId);

            //Assert
            Assert.NotNull(returnedResponse);
            Assert.True(returnedResponse.Success);
            Assert.True(startingFeedometer < testDragon4.Feedometer);
        }

        [Fact]
        public async Task ReturnDeadResponseOnIncreaseFeedometerCallIfDragonNotAlive()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon5");
            var testDragon5 = _lifeCycleManager.GetDragonByIdAsync(testDragonId).Result!;
            testDragon5.IsAlive = false;
            const FeedingFailureReason expectedReason = FeedingFailureReason.Dead;
            var startingFeedometer = testDragon5.Feedometer;

            //Act
            var returnedResponse = await _lifeCycleManager.IncreaseFeedometerAsync(testDragonId);

            //Assert
            Assert.NotNull(returnedResponse);
            Assert.False(returnedResponse.Success);
            Assert.Equal(expectedReason, returnedResponse.Reason.GetValueOrDefault());
            Assert.True(startingFeedometer == testDragon5.Feedometer);
        }

        [Fact]
        public async Task ReturnFullResponseOnIncreaseFeedometerCallIfFeedometerMoreThanMax()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon6");
            var testDragon6 = _lifeCycleManager.GetDragonByIdAsync(testDragonId).Result!;
            testDragon6.Feedometer = _gameSettings.BabySettings.MaxFeedometerForAgeGroup;
            const FeedingFailureReason expectedReason = FeedingFailureReason.Full;
            var startingFeedometer = testDragon6.Feedometer;

            //Act
            var returnedResponse = await _lifeCycleManager.IncreaseFeedometerAsync(testDragonId);

            //Assert
            Assert.NotNull(returnedResponse);
            Assert.False(returnedResponse.Success);
            Assert.Equal(expectedReason, returnedResponse.Reason.GetValueOrDefault());
            Assert.True(startingFeedometer == testDragon6.Feedometer);
        }

        [Fact]
        public async Task ReturnSuccessResponseOnIncreaseHappinessCallIfMaxHappinessNotReached()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon4");
            var testDragon4 = _lifeCycleManager.GetDragonByIdAsync(testDragonId).Result!;
            var startingHappiness = testDragon4.Happiness;

            //Act
            var returnedResponse = await _lifeCycleManager.IncreaseHappinessAsync(testDragonId);

            //Assert
            returnedResponse.Should().NotBeNull();
            returnedResponse.Success.Should().BeTrue();
            testDragon4.Happiness.Should().BeGreaterThan(startingHappiness);
        }

        [Fact]
        public async Task ReturnDeadResponseOnIncreaseHappinessCallIfDragonNotAlive()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon5");
            var testDragon5 = _lifeCycleManager.GetDragonByIdAsync(testDragonId).Result!;
            testDragon5.IsAlive = false;
            const PettingFailureReason expectedReason = PettingFailureReason.Dead;
            var startingHappiness = testDragon5.Happiness;

            //Act
            var returnedResponse =await _lifeCycleManager.IncreaseHappinessAsync(testDragonId);

            //Assert
            returnedResponse.Should().NotBeNull();
            returnedResponse.Success.Should().BeFalse();
            returnedResponse.Reason.Should().Be(expectedReason);
            testDragon5.Happiness.Should().Be(startingHappiness);
        }

        [Fact]
        public async Task ReturnOverpettedResponseOnIncreaseFeedometerCallIfFeedometerMoreThanMax()
        {
            //Arrange
            var testDragonId = _lifeCycleManager.CreateDragon("TestDragon6");
            var testDragon6 = _lifeCycleManager.GetDragonByIdAsync(testDragonId).Result!;
            testDragon6.Happiness = _gameSettings.BabySettings.MaxHappinessForAgeGroup;
            const PettingFailureReason expectedReason = PettingFailureReason.Overpetted;
            var startingHappiness = testDragon6.Happiness;

            //Act
            var returnedResponse = await  _lifeCycleManager.IncreaseHappinessAsync(testDragonId);

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