namespace Tamagotchi
{
    internal interface ILifeCycleManager
    {
        string IncreaseFeedometer(Dragon dragon);
        string IncreaseHappiness(Dragon dragon);
        void ProgressLifeSettings(Dragon dragon);
    }
}