namespace Tamagotchi
{
    public class TamagotchiApi
    {
        private readonly ILifeCycleManager _lifeCycleManager;

        public TamagotchiApi(ILifeCycleManager lifeCycleManager)
        {
            _lifeCycleManager = lifeCycleManager;
        }

        public Guid StartGame(string name)
        {
            return _lifeCycleManager.CreateDragon(name);
        }

        public FeedDragonResponse FeedDragon(Guid dragonId)
        {
            return _lifeCycleManager.IncreaseFeedometer(dragonId);
        }

        public PetDragonResponse PetDragon(Guid dragonId)
        {
            return _lifeCycleManager.IncreaseHappiness(dragonId);
        }

        public Dragon GetGameStatus(Guid dragonId) 
        {
            return _lifeCycleManager.GetDragonById(dragonId);
        }
    }
}
