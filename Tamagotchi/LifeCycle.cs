﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
        readonly IConsoleManager _consoleManager;
        private readonly ILifeCycleManager _lifeCycleManager;
        private readonly GameSettings _gameSettings;
        private readonly DragonMessages _dragonMessages;
        private int? _exitCode;

        public LifeCycle(IHostApplicationLifetime hostApplicationLifetime, ILifeCycleManager lifeCycleManager,
            IOptions<GameSettings> gameSettings, IConsoleManager consoleManager, IOptions<DragonMessages> dragonMessages)
        {
            _consoleManager = consoleManager;
            _lifeCycleManager = lifeCycleManager;
            _gameSettings = gameSettings.Value;

            _dragon = new Dragon()
            {
                Feedometer = _gameSettings.InitialFeedometer,
                Happiness = _gameSettings.InitialHappiness
            };

            _hostApplicationLifetime = hostApplicationLifetime;
            _dragonMessages = dragonMessages.Value;
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
                    catch (Exception)
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
            _gameStatusTimer.Interval = _gameSettings.GameStatusTimerInterval;
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
                    dragonsMessage = _dragonMessages.WrongKey;
                }

                _consoleManager.WriteDragonsMessage(dragonsMessage);
            }
        }

        public void ScheduleLifeProgressTimer()
        {
            _lifeProgressTimer.Interval = _gameSettings.LifeProgressTimerInterval;
            _lifeProgressTimer.Elapsed += new ElapsedEventHandler(ProgressLife);
            _lifeProgressTimer.Start();
        }

        public void ProgressLife(object? sender, ElapsedEventArgs e)
        {
            _lifeCycleManager.ProgressLifeSettings(_dragon);

            if (_dragon.Feedometer <= _gameSettings.MinValueOfFeedometer
                || _dragon.Happiness <= _gameSettings.MinValueOfHappiness
                || _dragon.Age >= _gameSettings.MaxAge) 
            {
                _dragon.IsAlive = false;
                _lifeProgressTimer.Dispose();
            }
        }
    }
}
