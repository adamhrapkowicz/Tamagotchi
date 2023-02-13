namespace Tamagotchi
{
    internal interface ILifeCycleManager
    {
        string IncreaseFeedometer(Dragon dragon);

        string IncreaseHappiness(Dragon dragon);

        void ProgressLifeSettings(Dragon dragon);

        Dictionary<string, int> CareLevelManager(Dragon dragon);

        Dictionary<string, int> SetInitialDragonsValues();

        Dictionary<string, double> GameOverValues();
    }
}