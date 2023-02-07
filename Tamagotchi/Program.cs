namespace Tamagotchi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Dragon dragon = new();

            dragon.BirthOfTheDragon();

            await LifeCycle.RunLifeCycle(dragon);

            dragon.DeathOfTheDragon();

        }
    }
}