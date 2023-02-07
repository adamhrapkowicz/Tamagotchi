namespace Tamagotchi
{
    public class LifeCycle
    {
        private readonly Dragon _dragon;

        public LifeCycle(Dragon dragon)
        {
            _dragon = dragon;
        }

        public async Task RunLifeCycle(n)
        {
            var letUserFeedAndPetDragonThread = Task.Run(() => LetUserFeedAndPetDragon());
            var decreaseFeedometerAndHappinessThread = Task.Run(() => DecreaseFedometerAndHappiness());

            await Task.WhenAll(decreaseFeedometerAndHappinessThread, letUserFeedAndPetDragonThread);
        }


        public void LetUserFeedAndPetDragon()
        {
            while (_dragon.IsAlive)
            {
                Console.WriteLine($"Value of happiness is {_dragon.Happiness} and value of feedometer is {_dragon.Feedometer}.");

                Console.WriteLine("To feed press 1, to pet press 2.");
                var userAction = Console.ReadLine();
                if (userAction == "1")
                {
                    _dragon.Feedometer++;
                    Console.WriteLine($"That was yummy!");
                }
                else if (userAction == "2")
                {
                    _dragon.Happiness++;
                    Console.WriteLine($"I love you!");
                }
                else
                {
                    Console.WriteLine($"You pressed the wrong button. Your dragon didn't get fed and wasn't petted.");
                }
            }
        }

        public void DecreaseFedometerAndHappiness()
        {
            while (_dragon.Feedometer != 0 && _dragon.Happiness != 0)
            {
                _dragon.Feedometer--;
                _dragon.Happiness--;
                Console.WriteLine($"Value2 of happiness is {_dragon.Happiness} and value of feedometer is {_dragon.Feedometer}.");
                Thread.Sleep(6000);
            }

            _dragon.IsAlive = false;
        }
    }
}
