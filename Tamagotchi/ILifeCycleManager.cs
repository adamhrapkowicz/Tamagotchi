using Tamagotchi.Contracts;
using TamagotchiData.Models;

namespace Tamagotchi
{
    public interface ILifeCycleManager
    {
        Task<FeedDragonResponse> IncreaseFeedometerAsync(Guid dragonId);

        Task<PetDragonResponse> IncreaseHappinessAsync(Guid dragonId);

        Task<Dragon?> GetDragonByIdAsync(Guid dragonId);

        Guid CreateDragon(string name);

        GameStatusResponse GetGameStatus(Guid dragonId);

        AgeGroupSettings GetCareLevelsForAgeGroups(Dragon dragon);
    }
}