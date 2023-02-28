using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Timers;

namespace Tamagotchi
{
    public class LifeCycle : IHostedService
    {
        private static readonly System.Timers.Timer LifeProgressTimer = new();
        
        private readonly ILifeCycleManager _lifeCycleManager;
        private readonly GameSettings _gameSettings;
        
        private readonly int? _exitCode;

        public LifeCycle(ILifeCycleManager lifeCycleManager, IOptions<GameSettings> gameSettings)
        {
            _lifeCycleManager = lifeCycleManager;
            _gameSettings = gameSettings.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ScheduleLifeProgressTimer();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
            return Task.CompletedTask;
        }

        private void ScheduleLifeProgressTimer()
        {
            LifeProgressTimer.Interval = _gameSettings.LifeProgressTimerInterval;
            LifeProgressTimer.Elapsed += ProgressLife;
            LifeProgressTimer.Start();
        }

        private void ProgressLife(object? sender, ElapsedEventArgs e)
        {
            _lifeCycleManager.ProgressLife();
        }
    }
}
