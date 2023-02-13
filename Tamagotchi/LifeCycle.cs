using System.Timers;

namespace Tamagotchi
{
    public class LifeCycle
    {
        private readonly Dragon _dragon;
        static readonly System.Timers.Timer _gameStatusTimer = new(300);
        static readonly System.Timers.Timer _lifeProgressTimer = new(700);
        readonly IConsoleManager _consoleManager = new ConsoleManager();
        readonly ILifeCycleManager _lifeCycleManager = new LifeCycleManager();

        public LifeCycle()
        {
            _dragon = new Dragon()
            {
                Feedometer = _lifeCycleManager.SetInitialDragonsValues()["Feedometer"],
                Happiness = _lifeCycleManager.SetInitialDragonsValues()["Happiness"],
            };
        }

        public async Task RunLifeCycle()
        {
            DeclareBirthOfTheDragon();

            var letUserCareForDragonTask = Task.Run(() => LetUserCareForDragon());
            var progressLifeTask = Task.Run(() => ScheduleLifeProgressTimer());
            var displayGameStatusTask = Task.Run(() => ScheduleGameStatusTimer());

            await Task.WhenAll(progressLifeTask, letUserCareForDragonTask, displayGameStatusTask);
        }

        public void DeclareBirthOfTheDragon()
        {
            string? inputName = _consoleManager.GetDragonNameFromUser();

            if (inputName != null && inputName != "")
            {
                _dragon.Name = inputName;
            }
        }

        public void DeclareDeathOfTheDragon()
        {
            _consoleManager.WriteDeclarationOfDeath(_dragon);
            _gameStatusTimer.Stop();
        }

        public void ScheduleGameStatusTimer()
        {
            _gameStatusTimer.Elapsed += new ElapsedEventHandler(DisplayGameStatus);
            _gameStatusTimer.Start();
        }

        private void DisplayGameStatus(object? sender, ElapsedEventArgs e)
        {
            if (!_dragon.IsAlive)
            {
                DeclareDeathOfTheDragon();
                return;
            }

            _consoleManager.WriteGameStatus(_dragon);
        }

        public void LetUserCareForDragon()
        {
            while (_dragon.IsAlive)
            {
                var careInstructionsFromUser = _consoleManager.GetCareInstructionsFromUser();
                string? dragonsmessage;

                if (careInstructionsFromUser == "1")
                {
                    dragonsmessage = _lifeCycleManager.IncreaseFeedometer(_dragon);
                }
                else if (careInstructionsFromUser == "2")
                {
                    dragonsmessage = _lifeCycleManager.IncreaseHappiness(_dragon);
                }
                else
                {
                    dragonsmessage = "Where is my snack? Do you still love me?";
                }

                _consoleManager.DragonsMessage(dragonsmessage);
            }
        }

        public void ScheduleLifeProgressTimer()
        {
            _lifeProgressTimer.Elapsed += new ElapsedEventHandler(ProgressLife);
            _lifeProgressTimer.Start();
        }

        public void ProgressLife(object sender, ElapsedEventArgs e)
        {
            _lifeCycleManager.ProgressLifeSettings(_dragon);

            if (_dragon.Feedometer <= 0 || _dragon.Happiness <= 0)
            {
                _dragon.IsAlive = false;
            }
        }
    }
}
