namespace Tamagotchi
{
    public interface ILifeCycleManager
    {
        string IncreaseFeedometer(Dragon dragon);

        string IncreaseHappiness(Dragon dragon);

        void ProgressLifeSettings(Dragon dragon);

        Dictionary<string, int> SetCareLevelsForAgeGroups(Dragon dragon);

        Dictionary<string, int> SetInitialDragonsValues();

        Dictionary<string, double> SetGameOverValues();
        
        Dictionary<string, double> SetTimersIntervals();
    }
}