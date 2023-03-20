using Microsoft.Extensions.Options;
using Tamagotchi.Contracts;
using TamagotchiData.Models;

namespace Tamagotchi
{
    public class LifeCycleManager : ILifeCycleManager
    {
        private readonly GameSettings _gameSettings;
        private readonly TamagotchiDbContext _tamagotchiDbContext;

        public LifeCycleManager(IOptions<GameSettings> gameSettings, TamagotchiDbContext tamagotchiDbContext)
        {
            _tamagotchiDbContext = tamagotchiDbContext;
            _gameSettings = gameSettings.Value;
        }

        public FeedDragonResponse IncreaseFeedometer(Guid dragonId)
        {
            var dragon = GetDragonById(dragonId);

            if (dragon.IsAlive == false)
                return new FeedDragonResponse { Success = false, Reason = FeedingFailureReason.Dead };

            if (dragon.Feedometer >= GetCareLevelsForAgeGroups(dragon).MaxFeedometerForAgeGroup)
                return new FeedDragonResponse { Success = false, Reason = FeedingFailureReason.Full };

            dragon.Feedometer += GetCareLevelsForAgeGroups(dragon).FeedometerIncrement;

            _tamagotchiDbContext.SaveChanges();

            return new FeedDragonResponse { Success = true };
        }

        public PetDragonResponse IncreaseHappiness(Guid dragonId)
        {
            var dragon = GetDragonById(dragonId);

            if (dragon.IsAlive == false)
                return new PetDragonResponse { Success = false, Reason = PettingFailureReason.Dead };

            if (dragon.Happiness >= GetCareLevelsForAgeGroups(dragon).MaxHappinessForAgeGroup)
                return new PetDragonResponse { Success = false, Reason = PettingFailureReason.Overpetted };

            dragon.Happiness += GetCareLevelsForAgeGroups(dragon).HappinessIncrement;

            _tamagotchiDbContext.SaveChanges();

            return new PetDragonResponse { Success = true };
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

            _tamagotchiDbContext.Dragons.Add(dragon);
            _tamagotchiDbContext.SaveChanges();

            return dragon.DragonId;
        }

        public Dragon GetDragonById(Guid dragonId)
        {
            return _tamagotchiDbContext.Dragons.FirstOrDefault(d => d.DragonId == dragonId)!;
        }

        public GameStatusResponse GetGameStatus(Guid dragonId)
        {
            var dragon = GetDragonById(dragonId);

            return !dragon.IsAlive
                ? new GameStatusResponse { Success = false, Reason = GetGameStatusFailureReason.Dead, StatusDragon = dragon}
                : new GameStatusResponse { Success = true, StatusDragon = dragon };
        }

        public AgeGroupSettings GetCareLevelsForAgeGroups(Dragon dragon)
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
