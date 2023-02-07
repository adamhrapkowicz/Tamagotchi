using System.Timers;

namespace Tamagotchi
{
    public class LifeCycle
    {
        private readonly Dragon _dragon;
        static System.Timers.Timer _timer;

        public LifeCycle(Dragon dragon)
        {
            _dragon = dragon;
        }

        public void ScheduleDecreaseFeedometerAndHappinessTimer()
        {
            _timer = new System.Timers.Timer(6000);
            _timer.Elapsed += new ElapsedEventHandler(DecreaseFedometerAndHappiness);
            _timer.Start();
        }

        public async Task RunLifeCycle()
        {
            var letUserFeedAndPetDragonTask = Task.Run(() => LetUserFeedAndPetDragon());
            var decreaseFeedometerAndHappinessTask = Task.Run(() => ScheduleDecreaseFeedometerAndHappinessTimer());

            await Task.WhenAll(decreaseFeedometerAndHappinessTask, letUserFeedAndPetDragonTask);
        }

        public void LetUserFeedAndPetDragon()
        {
            while (_dragon.IsAlive && _dragon.Happiness != 0 && _dragon.Feedometer != 0)
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

        public void DecreaseFedometerAndHappiness(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();

            if (_dragon.Feedometer != 0 && _dragon.Happiness != 0)
            {
                _dragon.Feedometer--;
                _dragon.Happiness--;
                Console.WriteLine($"Value2 of happiness is {_dragon.Happiness} and value of feedometer is {_dragon.Feedometer}.");

                ScheduleDecreaseFeedometerAndHappinessTimer();
            }
            else
            {

                _dragon.IsAlive = false;
            }
        }
    }
}
