using Tamagotchi.Contracts;
using TamagotchiData.Models;

namespace Tamagotchi
{
    public interface ILifeCycleManager
    {
        FeedDragonResponse IncreaseFeedometer(Guid dragonId);

        Task<PetDragonResponse> IncreaseHappinessAsync(Guid dragonId);

        Dragon GetDragonById(Guid dragonId);

        Guid CreateDragon(string name);

        GameStatusResponse GetGameStatus(Guid dragonId);

        AgeGroupSettings GetCareLevelsForAgeGroups(Dragon dragon);
    }
}