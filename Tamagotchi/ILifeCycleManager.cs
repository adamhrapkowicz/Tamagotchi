namespace Tamagotchi
{
    public interface ILifeCycleManager
    {
        FeedDragonResponse IncreaseFeedometer(Guid dragonId);

        PetDragonResponse IncreaseHappiness(Guid dragonId);

        void ProgressLifeSettings();

        Dragon GetDragonById(Guid dragonId);

        Guid CreateDragon(string name);

        AgeGroupSettings SetCareLevelsForAgeGroups(Dragon dragon);
    }
}