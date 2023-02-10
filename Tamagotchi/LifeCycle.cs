using System.Timers;

namespace Tamagotchi
{
    public class LifeCycle
    {
        private readonly Dragon _dragon;
        static readonly System.Timers.Timer _statusTimer = new(300);
        static readonly System.Timers.Timer _lifeTimer = new(700);
        readonly IConsoleManager _consoleManager = new ConsoleManager();

        public LifeCycle()
        {
            _dragon = new Dragon();
        }

        public async Task RunLifeCycle()
        {
            DeclareBirthOfTheDragon();

            var letUserFeedAndPetDragonTask = Task.Run(() => LetUserFeedAndPetDragon());
            var decreaseFeedometerAndHappinessTask = Task.Run(() => ScheduleDecreaseFeedometerAndHappinessTimer());
            var displayStatusOfFeedometerAndHappinessTask = Task.Run(() => ScheduleDisplayStatusOfFedometerAndHappinessTimer());

            await Task.WhenAll(decreaseFeedometerAndHappinessTask, letUserFeedAndPetDragonTask, displayStatusOfFeedometerAndHappinessTask);
        }

        public void DeclareBirthOfTheDragon()
        {
            string? inputName = _consoleManager.GetDragonNameFromUser();

            if (inputName != null && inputName != "")
            {
                _dragon.Name = inputName;
            }
            else
            {
                _dragon.Happiness -= 1;
            }
        }

        public void DeclareDeathOfTheDragon()
        {
            _consoleManager.PrintDeclarationOfDeath(_dragon);
            _statusTimer.Stop();
        }

        public void ScheduleDisplayStatusOfFedometerAndHappinessTimer()
        {
            _statusTimer.Elapsed += new ElapsedEventHandler(DisplayStatusOfFeedometerAndHappiness);
            _statusTimer.Start();
        }

        private void DisplayStatusOfFeedometerAndHappiness(object? sender, ElapsedEventArgs e)
        {
            if (!_dragon.IsAlive)
            {
                DeclareDeathOfTheDragon();
                return;
            }

            _consoleManager.WriteGameStatus(_dragon);
        }

        public void LetUserFeedAndPetDragon()
        {
            while (_dragon.IsAlive)
            {
                var careInstructionsFromUser = _consoleManager.GetCareInstructionsFromUser();
                string? dragonsmessage;

                if (careInstructionsFromUser == "1")
                {
                    _dragon.Feedometer += 50;

                    dragonsmessage = "That was yummy!";

                }
                else if (careInstructionsFromUser == "2")
                {
                    _dragon.Happiness += 50;

                    dragonsmessage = "I love you!";
                }
                else
                {
                    dragonsmessage = "Where is my snack? Do you still love me?";
                }

                _consoleManager.DragonsMessage(dragonsmessage);
            }
        }

        public void ScheduleDecreaseFeedometerAndHappinessTimer()
        {
            _lifeTimer.Elapsed += new ElapsedEventHandler(DecreaseFedometerAndHappiness);
            _lifeTimer.Start();
        }

        public void DecreaseFedometerAndHappiness(object sender, ElapsedEventArgs e)
        {
            _dragon.Age += 0.1;
            _dragon.Feedometer--;
            _dragon.Happiness--;

            if (_dragon.Feedometer <= 0 || _dragon.Happiness <= 0)
            {
                _dragon.IsAlive = false;
            }
        }
    }
}
