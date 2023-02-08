﻿using System.Timers;

namespace Tamagotchi
{
    public class LifeCycle
    {
        private readonly Dragon _dragon;
        static System.Timers.Timer _timer, _timer2;

        public LifeCycle(Dragon dragon)
        {
            _dragon = dragon;
        }

        public async Task RunLifeCycle()
        {
            var letUserFeedAndPetDragonTask = Task.Run(() => LetUserFeedAndPetDragon());
            var decreaseFeedometerAndHappinessTask = Task.Run(() => ScheduleDecreaseFeedometerAndHappinessTimer());
            var displayStatusTask = Task.Run(() => ScheduleDisplayStatusTimer());

            await Task.WhenAll(decreaseFeedometerAndHappinessTask, letUserFeedAndPetDragonTask, displayStatusTask);
        }

        public void ScheduleDisplayStatusTimer()
        {
            _timer2 = new System.Timers.Timer(3000);
            _timer2.Elapsed += new ElapsedEventHandler(DisplayStatus);
            _timer2.Start();
        }

        private void DisplayStatus(object? sender, ElapsedEventArgs e)
        {
            Console.Clear();
            Console.WriteLine("To feed press 1, to pet press 2.");
            Console.WriteLine($"Value of happiness is {_dragon.Happiness} and value of feedometer is {_dragon.Feedometer}.");

            if (! _dragon.IsAlive)
            {
                _timer2.Stop();
            }
        }

        public void LetUserFeedAndPetDragon()
        {
            while (_dragon.IsAlive)
            {
                //Console.WriteLine("To feed press 1, to pet press 2.");
                //Console.WriteLine($"Value of happiness is {_dragon.Happiness} and value of feedometer is {_dragon.Feedometer}.");

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

        public void ScheduleDecreaseFeedometerAndHappinessTimer()
        {
            _timer = new System.Timers.Timer(6000);
            _timer.Elapsed += new ElapsedEventHandler(DecreaseFedometerAndHappiness);
            _timer.Start();
        }

        public void DecreaseFedometerAndHappiness(object sender, ElapsedEventArgs e)
        {
            _dragon.Feedometer--;
            _dragon.Happiness--;

            Console.WriteLine($"Value2 of happiness is {_dragon.Happiness} and value of feedometer is {_dragon.Feedometer}.");

            if (_dragon.Feedometer == 0 || _dragon.Happiness == 0)
            {
                _dragon.IsAlive = false;
                _timer.Stop();
            }
        }
    }
}
