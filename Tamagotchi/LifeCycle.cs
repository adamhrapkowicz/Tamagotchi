using System.Timers;

namespace Tamagotchi
{
    public class LifeCycle
    {
        private readonly Dragon _dragon;
        static readonly System.Timers.Timer _gameStatusTimer = new();
        static readonly System.Timers.Timer _lifeProgressTimer = new();
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
            Environment.Exit(0);
        }

        public void ScheduleGameStatusTimer()
        {
            _gameStatusTimer.Interval = _lifeCycleManager.SetTimersIntervals()["GameStatusTimerInterval"];
            _gameStatusTimer.Elapsed += new ElapsedEventHandler(DisplayGameStatus);
            _gameStatusTimer.Start();
        }

        private void DisplayGameStatus(object? sender, ElapsedEventArgs e)
        {
            if (!_dragon.IsAlive)
            {
                _gameStatusTimer.Dispose();
                DeclareDeathOfTheDragon();
            }

            _consoleManager.WriteGameStatus(_dragon);
        }

        public void LetUserCareForDragon()
        {
            while (_dragon.IsAlive)
            {
                var careInstructionsFromUser = _consoleManager.GetCareInstructionsFromUser();
                string? dragonsMessage;

                if (careInstructionsFromUser == "1")
                {
                    dragonsMessage = _lifeCycleManager.IncreaseFeedometer(_dragon);
                }
                else if (careInstructionsFromUser == "2")
                {
                    dragonsMessage = _lifeCycleManager.IncreaseHappiness(_dragon);
                }
                else
                {
                    dragonsMessage = "Where is my snack? Do you still love me?";
                }

                _consoleManager.WriteDragonsMessage(dragonsMessage);
            }
        }

        public void ScheduleLifeProgressTimer()
        {
            _lifeProgressTimer.Interval = _lifeCycleManager.SetTimersIntervals()["LifeProgressTimerInterval"]; 
            _lifeProgressTimer.Elapsed += new ElapsedEventHandler(ProgressLife);
            _lifeProgressTimer.Start();
        }

        public void ProgressLife(object sender, ElapsedEventArgs e)
        {
            _lifeCycleManager.ProgressLifeSettings(_dragon);

            if (_dragon.Feedometer <= _lifeCycleManager.SetGameOverValues()["minValueOfFeedometer"]
                || _dragon.Happiness <= _lifeCycleManager.SetGameOverValues()["minValueOfHappiness"]
                || _dragon.Age >= _lifeCycleManager.SetGameOverValues()["maxAge"])
            {
                _dragon.IsAlive = false;
                _lifeProgressTimer.Dispose();
            }
        }
    }
}
