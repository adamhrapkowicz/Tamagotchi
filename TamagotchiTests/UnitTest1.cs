using Microsoft.Extensions.Options;
using Tamagotchi;
using TamagotchiTests.Mocks;

namespace TamagotchiTests
{
    public class UnitTest1
    {
        private readonly ILifeCycleManager _lifeCycleManager;

        [Fact]
        public void Test1()
        {
            //Arrange
            string name = "testdragon";

            //Act

            var result = _lifeCycleManager.CreateDragon(name);

            //Assert
            Assert.IsType<Guid>(result);

        }

        [Fact]
        public void Test2()
        {
            string[] mockDragonsNames = new string[] { "MockDragon1", "MockDragon2", "MockDragon3" };
            foreach (var name in mockDragonsNames)
            {
                Guid mockDragonId = DragonsMocks.CreateMockDragon(name);
            }

            //List<Dragon> _dragons = new();
            //string name1 = "testDragon1";
            //string name2 = "testDragon2";
            //string name3 = "testDragon3";
            //var testDragon1Id = _lifeCycleManager.CreateDragon(name1);
            //var testDragon2Id = _lifeCycleManager.CreateDragon(name2);
            //var testDragon3Id = _lifeCycleManager.CreateDragon(name3);
            foreach (var mockDragon in DragonsMocks._mockDragons)
            //Guid testDragon4Id = Guid.NewGuid();
            {
                var result = _lifeCycleManager.GetDragonById(mockDragon.DragonId);
                Assert.IsType<Dragon>(result);
            }
        }
    }
}