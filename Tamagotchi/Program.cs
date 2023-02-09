namespace Tamagotchi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var lifeCycle = new LifeCycle();

            await lifeCycle.RunLifeCycle();
        }
    }
}