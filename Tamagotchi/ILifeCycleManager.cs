namespace Tamagotchi
{
    public interface ILifeCycleManager
    {
        string IncreaseFeedometer(Dragon dragon);

        string IncreaseHappiness(Dragon dragon);

        void ProgressLifeSettings(Dragon dragon);

        AgeGroupSettings SetCareLevelsForAgeGroups(Dragon dragon);
    }
}