namespace Tamagotchi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dragon = new Dragon();
            dragon.IsAlive = true;
            dragon.Happiness = 5;
            dragon.Feedometer = 1;
            while (dragon.IsAlive)
            {
                if (dragon.Happiness == 0 || dragon.Feedometer == 0)
                {
                    dragon.IsAlive = false;
                }
                //Console.WriteLine("To check age press a, to check feedometer press f, to check if it is happy press h");
                Console.WriteLine("To feed press 1, to pet press 2");
                var usersAction = Console.ReadLine().ToString();
                if (usersAction == "1") 
                {
                    dragon.Feedometer += 1;
                }
                else if (usersAction == "2")
                {
                    dragon.Happiness += 1;
                }
                else
                {
                    Console.WriteLine($"You pressed the wrong button. Your dragon didn't get fed and wasn't petted");
                }
            }
        }
    }
}