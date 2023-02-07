namespace Tamagotchi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Dragon dragon = new();

            dragon.BirthOfTheDragon();

            var lifeCycle = new LifeCycle(dragon);

            await lifeCycle.RunLifeCycle();

            dragon.DeathOfTheDragon();

        }
    }
}