namespace Tamagotchi
{
    public interface ILifeCycleManager
    {
        FeedDragonResponse IncreaseFeedometer(Guid dragonId);

        PetDragonResponse IncreaseHappiness(Guid dragonId);

        void ProgressLifeSettings();

        Dragon GetDragonById(Guid dragonId);
    }
}