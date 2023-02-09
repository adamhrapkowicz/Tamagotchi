using System.Timers;

namespace Tamagotchi
{
    public class LifeCycle
    {
        private readonly Dragon _dragon;
        static System.Timers.Timer _statusTimer = new System.Timers.Timer(300);
        static System.Timers.Timer _lifeTimer = new System.Timers.Timer(700);
        IConsoleManager _consoleManager = new ConsoleManager();

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

            DeclareDeathOfTheDragon();
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
            
            if (!_dragon.IsAlive)
            {
                _statusTimer.Stop();
            }
        }

        public void LetUserFeedAndPetDragon()
        {
            while (_dragon.IsAlive)
            {
                var careInstructionsFromUser = _consoleManager.GetCareInstructionsFromUser();
                var dragonsmessage = "";

                if (!_dragon.IsAlive)
                    return;

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
                _lifeTimer.Stop();
            }
        }
    }
}
