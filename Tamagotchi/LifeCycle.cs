namespace Tamagotchi
{
    public class LifeCycle
    {
        public static async Task RunLifeCycle(Dragon dragon)
        {
            var letUserFeedAndPetDragonThread = Task.Run(() => LetUserFeedAndPetDragon(dragon));
            var decreaseFeedometerAndHappinessThread = Task.Run(() => DecreaseFedometerAndHappiness(dragon));

            await Task.WhenAll(decreaseFeedometerAndHappinessThread, letUserFeedAndPetDragonThread);
        }


        public static void LetUserFeedAndPetDragon(Dragon dragon)
        {
            while (true)
            {
                while (dragon.IsAlive)
                {
                    Console.WriteLine($"Value of happiness is {dragon.Happiness} and value of feedometer is {dragon.Feedometer}.");

                    Console.WriteLine("To feed press 1, to pet press 2.");
                    var userAction = Console.ReadLine();
                    if (userAction == "1")
                    {
                        dragon.Feedometer++;
                        Console.WriteLine($"That was yummy!");
                    }
                    else if (userAction == "2")
                    {
                        dragon.Happiness++;
                        Console.WriteLine($"I love you!");
                    }
                    else
                    {
                        Console.WriteLine($"You pressed the wrong button. Your dragon didn't get fed and wasn't petted.");
                    }
                }
                break;

            }
        }

        public static void DecreaseFedometerAndHappiness(Dragon dragon)
        {
            while (true)
            {
                dragon.Feedometer--;
                dragon.Happiness--;
                Console.WriteLine($"Value2 of happiness is {dragon.Happiness} and value of feedometer is {dragon.Feedometer}.");
                if (dragon.Feedometer == 0 || dragon.Happiness == 0)
                {
                    dragon.IsAlive = false;
                    break;
                }
                else
                {
                    Thread.Sleep(6000);
                }
            }
        }
    }
}
