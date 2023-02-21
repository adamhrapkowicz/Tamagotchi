using Microsoft.Extensions.Options;

namespace Tamagotchi
{
    internal class LifeCycleManager : ILifeCycleManager
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

            if (dragon.Feedometer < SetCareLevelsForAgeGroups(dragon).MaxFeedometerForAgeGroup)
            {
                dragon.Feedometer += SetCareLevelsForAgeGroups(dragon).FeedometerIncrement;

                return new FeedDragonResponse { Success = true };
            }

            if (dragon.IsAlive == false)
            {
                return new FeedDragonResponse { Success = false, Reason = FeedingFailureReason.Dead };
            }

            return new FeedDragonResponse { Success = false, Reason = FeedingFailureReason.Full }; ;
        }

        public PetDragonResponse IncreaseHappiness(Guid dragonId)
        {
            var dragon = GetDragonById(dragonId);

            if (dragon.Happiness < SetCareLevelsForAgeGroups(dragon).MaxHappinessForAgeGroup)
            {
                dragon.Happiness += SetCareLevelsForAgeGroups(dragon).HappinessIncrement;

                return new PetDragonResponse { Success = true };
            }

            if (dragon.IsAlive == false)
            {
                return new PetDragonResponse { Success = false, Reason = PettingFailureReason.Dead };
            }

            return new PetDragonResponse { Success = false, Reason = PettingFailureReason.Overpetted };
        }

        public void ProgressLifeSettings()
        {
            foreach (var dragon in _dragons)
            {
                dragon.Age += _gameSettings.AgeIncrement;
                dragon.Feedometer -= SetCareLevelsForAgeGroups(dragon).HungerIncrement;

                if (dragon.Name == null && dragon.Name == "")
                {
                    dragon.Happiness -= SetCareLevelsForAgeGroups(dragon).SadnessIncrement * _gameSettings.NameNeglectPenalty;
                }

                dragon.Happiness -= SetCareLevelsForAgeGroups(dragon).SadnessIncrement;

                if (dragon.Feedometer <= _gameSettings.MinValueOfFeedometer
                    || dragon.Happiness <= _gameSettings.MinValueOfHappiness
                    || dragon.Age >= _gameSettings.MaxAge)
                {
                    dragon.IsAlive = false;
                }
            }
        }

        internal Guid CreateDragon(string name)
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
            return _dragons.FirstOrDefault(d => d.DragonId == dragonId);
        }

        private AgeGroupSettings SetCareLevelsForAgeGroups(Dragon dragon)
        {
            var ageGroup = dragon.AgeGroup;

            AgeGroupSettings gameSettingsForAgeGroup = ageGroup switch
            {
                AgeGroup.Baby => _gameSettings.BabySettings,
                AgeGroup.Child => _gameSettings.ChildSettings,
                AgeGroup.Teen => _gameSettings.TeenSettings,
                AgeGroup.Adult => _gameSettings.AdultSettings,
                AgeGroup.Senior => _gameSettings.SeniorSettings,
                _ => _gameSettings.SeniorSettings,
            };
            return gameSettingsForAgeGroup;
        }
    }
}
