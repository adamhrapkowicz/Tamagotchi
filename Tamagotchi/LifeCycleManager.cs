using Microsoft.Extensions.Options;
using Tamagotchi.Contracts;
using TamagotchiData.Models;

namespace Tamagotchi
{
    public class LifeCycleManager : ILifeCycleManager
    {
        private readonly GameSettings _gameSettings;
        private readonly List<Dragon> _dragons = new();

        public LifeCycleManager(IOptions<GameSettings> gameSettings)
        {
            _gameSettings = gameSettings.Value;
        }

        public FeedDragonResponse IncreaseFeedometer(Guid dragonId)
        {
            var dragon = GetDragonById(dragonId);

            if (dragon.IsAlive == false)
                return new FeedDragonResponse { Success = false, Reason = FeedingFailureReason.Dead };

            if (dragon.Feedometer >= SetCareLevelsForAgeGroups(dragon).MaxFeedometerForAgeGroup)
                return new FeedDragonResponse { Success = false, Reason = FeedingFailureReason.Full };

            dragon.Feedometer += SetCareLevelsForAgeGroups(dragon).FeedometerIncrement;

            return new FeedDragonResponse { Success = true };
        }

        public PetDragonResponse IncreaseHappiness(Guid dragonId)
        {
            var dragon = GetDragonById(dragonId);

            if (dragon.IsAlive == false)
                return new PetDragonResponse { Success = false, Reason = PettingFailureReason.Dead };

            if (dragon.Happiness >= SetCareLevelsForAgeGroups(dragon).MaxHappinessForAgeGroup)
                return new PetDragonResponse { Success = false, Reason = PettingFailureReason.Overpetted };
            
            dragon.Happiness += SetCareLevelsForAgeGroups(dragon).HappinessIncrement;

            return new PetDragonResponse { Success = true };
        }

        public void ProgressLife()
        {
            foreach (var dragon in _dragons.Where(p => p.IsAlive))
            {
                dragon.Age += _gameSettings.AgeIncrement;
                dragon.Feedometer -= SetCareLevelsForAgeGroups(dragon).HungerIncrement;
                dragon.Happiness -= SetCareLevelsForAgeGroups(dragon).SadnessIncrement;

                if (dragon.Feedometer <= _gameSettings.MinValueOfFeedometer
                    || dragon.Happiness <= _gameSettings.MinValueOfHappiness
                    || dragon.Age >= _gameSettings.MaxAge)
                {
                    dragon.IsAlive = false;
                }
            }
        }

        public Guid CreateDragon(string name)
        {
            Dragon dragon = new()
            {
                DragonId = Guid.NewGuid(),
                Name = name,
                Feedometer = _gameSettings.InitialFeedometer,
                Happiness = _gameSettings.InitialHappiness,
            };

            _dragons.Add(dragon);

            return dragon.DragonId;
        }

        public Dragon GetDragonById(Guid dragonId)
        {
            return _dragons.FirstOrDefault(d => d.DragonId == dragonId)!;
        }

        private AgeGroupSettings SetCareLevelsForAgeGroups(Dragon dragon)
        {
            var gameSettingsForAgeGroup = dragon.AgeGroup switch
            {
                AgeGroup.Baby => _gameSettings.BabySettings,
                AgeGroup.Child => _gameSettings.ChildSettings,
                AgeGroup.Teen => _gameSettings.TeenSettings,
                AgeGroup.Adult => _gameSettings.AdultSettings,
                AgeGroup.Senior => _gameSettings.SeniorSettings,
                _ => throw new Exception(
                    "Enum value not being properly handled in 'SetCareLevelsForAgeGroups(Dragon dragon)'"),
            };

            return gameSettingsForAgeGroup;
        }
    }
}
