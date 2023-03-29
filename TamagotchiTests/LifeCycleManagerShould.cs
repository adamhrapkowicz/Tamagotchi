using FluentAssertions;
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
        private readonly Mock<ITamagotchiRepository> _mockRepository = new();

        public LifeCycleManagerShould()
        {
            _gameSettings = GetMockSettings();
            var mockSettings = new Mock<IOptions<GameSettings>>();
            mockSettings.Setup(x => x.Value).Returns(_gameSettings);
           
            _lifeCycleManager = new LifeCycleManager(mockSettings.Object, _mockRepository.Object);
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

        private void MockDragons(IReadOnlyList<Guid> mockDragonIds)
        {
            var mockDragons = new List<Dragon>()
            {
                new()
                {
                    Name = "TestDragon",
                    DragonId = mockDragonIds[0],
                },
                new()
                {
                    Name = "OverfedDragon",
                    DragonId = mockDragonIds[1],
                    Feedometer = _gameSettings.BabySettings.MaxFeedometerForAgeGroup
                },
                new()
                {
                    Name = "OverpettedDragon",
                    DragonId = mockDragonIds[2],
                    Happiness = _gameSettings.BabySettings.MaxHappinessForAgeGroup
                },
                new()
                {
                    Name = "DeadDragon",
                    DragonId = mockDragonIds[3],
                    IsAlive = false
                }
            };

            _mockRepository.Setup(m => m.GetDragonAsync(It.IsAny<Guid>()).Result)
                .Returns((Guid id) => mockDragons.Find(d => d.DragonId == id));
        }

        [Fact]
        public void CreateDragonWithCorrectNameOnCreateDragonCall()
        {
            //Arrange
            const string name = "testDragon";

            //Act
            var response = _lifeCycleManager.CreateDragon(name);

            //Assert
            Assert.True(Guid.Empty != response.DragonId);
            _mockRepository.Verify(x=>x.AddDragonAsync(It.Is<Dragon>(p=>p.Name == name)));
        }

        [Fact]
        public async Task ReturnCorrectDragonOnGetDragonByIdAsyncCall()
        {
            //Arrange
            var mockDragonIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            MockDragons(mockDragonIds);

            const AgeGroup expectedAgeGroup = AgeGroup.Baby;
            const int expectedAge = 0;
            const string expectedName = "TestDragon";

            //Act
            var returnedDragon = await _lifeCycleManager.GetDragonByIdAsync(mockDragonIds[0]);

            //Assert
            _mockRepository.Verify(x => x.GetDragonAsync(It.IsNotNull<Guid>()));
            Assert.NotNull(returnedDragon);
            Assert.Equal(mockDragonIds[0], returnedDragon.DragonId);
            Assert.Equal(expectedName, returnedDragon.Name);
            Assert.Equal(expectedAgeGroup, returnedDragon.AgeGroup);
            Assert.Equal(expectedAge, returnedDragon.Age);
            Assert.True(returnedDragon.IsAlive);
        }

        [Fact]
        public void ReturnSuccessGameStatusResponseOnGetGameStatusCallWhenDragonIsAlive()
        {
            // Arrange
            var mockDragonIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            MockDragons(mockDragonIds);

            // Act
            var returnedResponse = _lifeCycleManager.GetGameStatus(mockDragonIds[0]);

            // Assert
            _mockRepository.Verify(x => x.GetDragonAsync(It.IsNotNull<Guid>()));
            returnedResponse.Should().NotBeNull();
            returnedResponse.Success.Should().BeTrue();
            returnedResponse.Reason.Should().BeNull();
        }

        [Fact]
        public async Task ReturnSuccessResponseOnIncreaseFeedometerAsyncCallIfMaxFeedometerNotReached()
        {
            //Arrange
            var mockDragonIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()};
            MockDragons(mockDragonIds);

            //Act
            var returnedResponse = await _lifeCycleManager.IncreaseFeedometerAsync(mockDragonIds[0]);

            //Assert
            _mockRepository.Verify(x =>x.SaveAllChangesAsync());
            Assert.NotNull(returnedResponse);
            Assert.True(returnedResponse.Success);
        }

        [Fact]
        public async Task ReturnDeadResponseOnIncreaseFeedometerAsyncCallIfDragonNotAlive()
        {
            //Arrange
            var mockDragonIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            MockDragons(mockDragonIds);

            const FeedingFailureReason expectedReason = FeedingFailureReason.Dead;


            //Act
            var returnedResponse = await _lifeCycleManager.IncreaseFeedometerAsync(mockDragonIds[3]);

            //Assert
            Assert.NotNull(returnedResponse);
            Assert.False(returnedResponse.Success);
            Assert.Equal(expectedReason, returnedResponse.Reason.GetValueOrDefault());
        }

        [Fact]
        public async Task ReturnFullResponseOnIncreaseFeedometerAsyncCallIfFeedometerMoreThanMax()
        {
            //Arrange
            var mockDragonIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            MockDragons(mockDragonIds);

            const FeedingFailureReason expectedReason = FeedingFailureReason.Full;

            //Act
            var returnedResponse = await _lifeCycleManager.IncreaseFeedometerAsync(mockDragonIds[1]);

            //Assert
            Assert.NotNull(returnedResponse);
            Assert.False(returnedResponse.Success);
            Assert.Equal(expectedReason, returnedResponse.Reason.GetValueOrDefault());
        }

        [Fact]
        public async Task ReturnSuccessResponseOnIncreaseHappinessAsyncCallIfMaxHappinessNotReached()
        {
            //Arrange
            var mockDragonIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            MockDragons(mockDragonIds);

            //Act
            var returnedResponse = await _lifeCycleManager.IncreaseHappinessAsync(mockDragonIds[0]);

            //Assert
            returnedResponse.Should().NotBeNull();
            returnedResponse.Success.Should().BeTrue();
            _mockRepository.Verify(x => x.SaveAllChangesAsync());
        }

        [Fact]
        public async Task ReturnDeadResponseOnIncreaseHappinessCallAsyncIfDragonNotAlive()
        {
            //Arrange
            var mockDragonIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            MockDragons(mockDragonIds);
            
            const PettingFailureReason expectedReason = PettingFailureReason.Dead;

            //Act
            var returnedResponse =await _lifeCycleManager.IncreaseHappinessAsync(mockDragonIds[3]);

            //Assert
            returnedResponse.Should().NotBeNull();
            returnedResponse.Success.Should().BeFalse();
            returnedResponse.Reason.Should().Be(expectedReason);
        }

        [Fact]
        public async Task ReturnOverpettedResponseOnIncreaseHappinessAsyncCallIfHappinessMoreThanMax()
        {
            //Arrange
            var mockDragonIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            MockDragons(mockDragonIds);

            const PettingFailureReason expectedReason = PettingFailureReason.Overpetted;

            //Act
            var returnedResponse = await  _lifeCycleManager.IncreaseHappinessAsync(mockDragonIds[2]);

            //Assert
            returnedResponse.Should().NotBeNull();
            returnedResponse.Success.Should().BeFalse();
            returnedResponse.Reason.Should().Be(expectedReason);
        }
    }
}