namespace Tamagotchi
{
    public interface ILifeCycleManager
    {
        FeedDragonResponse IncreaseFeedometer(Guid dragonId);

        PetDragonResponse IncreaseHappiness(Guid dragonId);

        void ProgressLife();

        Dragon GetDragonById(Guid dragonId);

        Guid CreateDragon(string name);
    }
}