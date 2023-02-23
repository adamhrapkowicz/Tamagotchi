using Microsoft.Extensions.Options;
using Moq;
using Tamagotchi;

namespace TamagotchiTests
{
    public class LifeCycleManagerShould
    {
        private readonly ILifeCycleManager _lifeCycleManager;

        public LifeCycleManagerShould()
        {
            var mockSettings = new Mock<IOptions<GameSettings>>();
            mockSettings.Setup(x => x.Value).Returns(new GameSettings
            {
                InitialFeedometer = 10,
                InitialHappiness = 10
            });

            _lifeCycleManager = new LifeCycleManager(mockSettings.Object);
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
            var expectedFeedometer = 10;
            var expectedHappiness = 10;

            
            //Act
            var testDragonId = _lifeCycleManager.CreateDragon(name);
            var returnedDragon = _lifeCycleManager.GetDragonById(testDragonId);

            //Assert
            Assert.IsType<Dragon>(returnedDragon);
            Assert.NotNull(returnedDragon);
            Assert.Equal(name, returnedDragon.Name);
            Assert.Equal(expectedAgeGroup, returnedDragon.AgeGroup);
            Assert.Equal(expectedAge, returnedDragon.Age);
            Assert.Equal(expectedFeedometer, returnedDragon.Feedometer);
            Assert.Equal(expectedHappiness, returnedDragon.Happiness);
            Assert.Equal(testDragonId, returnedDragon.DragonId);
            Assert.True(returnedDragon.IsAlive);
        }
    }
}