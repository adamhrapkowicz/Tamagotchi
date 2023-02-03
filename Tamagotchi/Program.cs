namespace Tamagotchi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dragon dragon = new();

            dragon.BirthOfTheDragon();

            LifeCycle.RunLifeCycle(dragon);

        }
    }
}