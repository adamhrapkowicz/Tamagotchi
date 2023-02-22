using Tamagotchi;

namespace TamagotchiTests.Mocks
{
    public class DragonsMocks
    {

        public static readonly List<Dragon> _mockDragons = new();

        public static Guid CreateMockDragon(string name)
        {

            Dragon mockDragon = new()
            {
                DragonId = Guid.NewGuid(),
                Feedometer = 10,
                Happiness = 10,
                Name = name, //"TestDrag1",
            };
            _mockDragons.Add(mockDragon);
            return mockDragon.DragonId;
        }

       
    }

}
