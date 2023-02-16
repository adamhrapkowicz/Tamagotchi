using Microsoft.Extensions.Hosting;
using System.Timers;

namespace Tamagotchi
{
    public class LifeCycle : IHostedService
    {
        private readonly Dragon _dragon;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private Task? _applicationTask;
        static readonly System.Timers.Timer _gameStatusTimer = new();
        static readonly System.Timers.Timer _lifeProgressTimer = new();
        readonly IConsoleManager _consoleManager = new ConsoleManager();
        private readonly ILifeCycleManager _lifeCycleManager;
        private int? _exitCode;

        public LifeCycle(IHostApplicationLifetime hostApplicationLifetime, ILifeCycleManager lifeCycleManager)
        {
            _lifeCycleManager = lifeCycleManager;

            _dragon = new Dragon()
            {
                Feedometer = _lifeCycleManager.SetInitialDragonsValues()["Feedometer"],
                Happiness = _lifeCycleManager.SetInitialDragonsValues()["Happiness"],
            };

            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            CancellationTokenSource? _cancellationTokenSource = null;

            _hostApplicationLifetime.ApplicationStarted.Register(() =>
            {
                _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                _applicationTask = Task.Run(async () =>
                {
                    try
                    {
                        DeclareBirthOfTheDragon();

                        var letUserCareForDragonTask = Task.Run(() => LetUserCareForDragon());
                        var progressLifeTask = Task.Run(() => ScheduleLifeProgressTimer());
                        var displayGameStatusTask = Task.Run(() => ScheduleGameStatusTimer());

                        await Task.WhenAll(progressLifeTask, letUserCareForDragonTask, displayGameStatusTask);

                        _exitCode = 0;
                    }
                    catch (TaskCanceledException)
                    {

                    }
                    catch (Exception ex)
                    {
                        _exitCode = 1;
                    }
                    finally
                    {
                        _hostApplicationLifetime.StopApplication();
                    }
                }, cancellationToken);
            });

            _hostApplicationLifetime.ApplicationStopping.Register(() =>
            {
                _cancellationTokenSource?.Cancel();
            });

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_applicationTask != null) { await _applicationTask; }

            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
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
