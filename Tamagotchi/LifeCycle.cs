using System.Timers;
using Microsoft.Extensions.Options;
using TamagotchiData.Models;
using Timer = System.Timers.Timer;

namespace Tamagotchi;

public class LifeCycle : IHostedService
{
    private static readonly Timer LifeProgressTimer = new();

    private readonly int? _exitCode = 0;
    private readonly GameSettings _gameSettings;

    private readonly ILifeCycleManager _lifeCycleManager;
    private readonly IServiceProvider _serviceProvider;

    public LifeCycle(
        ILifeCycleManager lifeCycleManager, IOptions<GameSettings> gameSettings, IServiceProvider serviceProvider)
    {
        _lifeCycleManager = lifeCycleManager;
        _serviceProvider = serviceProvider;
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

    public void ScheduleLifeProgressTimer()
    {
        LifeProgressTimer.Interval = _gameSettings.LifeProgressTimerInterval;
        LifeProgressTimer.Elapsed += ProgressLife;
        LifeProgressTimer.Start();
    }

    private void ProgressLife(object? sender, ElapsedEventArgs e)
    {
        ProgressLife();
    }

    private void ProgressLife()
    {
        using var scope = _serviceProvider.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<ITamagotchiRepository>();

        foreach (var dragon in repository.Dragons!.Where(p => p.IsAlive))
        {
            dragon.Age += _gameSettings.AgeIncrement;
            dragon.Feedometer -= _lifeCycleManager.GetCareLevelsForAgeGroups(dragon).HungerIncrement;
            dragon.Happiness -= _lifeCycleManager.GetCareLevelsForAgeGroups(dragon).SadnessIncrement;

            if (dragon.Feedometer <= _gameSettings.MinValueOfFeedometer
                || dragon.Happiness <= _gameSettings.MinValueOfHappiness
                || dragon.Age >= _gameSettings.MaxAge)
                dragon.IsAlive = false;
        }
        repository.SaveAllChangesAsync().GetAwaiter().GetResult();
    }
}