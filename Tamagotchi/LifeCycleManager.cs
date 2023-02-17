using Microsoft.Extensions.Options;

namespace Tamagotchi
{
    internal class LifeCycleManager : ILifeCycleManager
    {
        private readonly IOptions<GameSettings> _gameSettings;
        public string dragonsMessage = string.Empty;

        public LifeCycleManager(IOptions<GameSettings> gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public Dictionary<string, int> SetInitialDragonsValues()
        {
            return new Dictionary<string, int> {
                { "Feedometer", _gameSettings.Value.Feedometer },
                { "Happiness", _gameSettings.Value.Happiness } 
            };
        }

        public Dictionary<string, double> SetGameOverValues()
        {
            return new Dictionary<string, double> {
                { "minValueOfFeedometer", _gameSettings.Value.MinValueOfFeedometer },
                { "minValueOfHappiness", _gameSettings.Value.MinValueOfHappiness },
                { "maxAge", _gameSettings.Value.MaxAge} };
        }

        public Dictionary<string, double> SetTimersIntervals()
        {
            return new Dictionary<string, double> {
                { "GameStatusTimerInterval", _gameSettings.Value.GameStatusTimerInterval },
                { "LifeProgressTimerInterval", _gameSettings.Value.LifeProgressTimerInterval } };
        }

        public AgeGroupSettings SetCareLevelsForAgeGroups(Dragon dragon)
        {
            var ageGroup = dragon.AgeGroup.ToString();
            AgeGroupSettings gameSettingsForAgeGroup = ageGroup switch
            {
                "Baby" => _gameSettings.Value.BabySettings,
                "Child" => _gameSettings.Value.ChildSettings,
                "Teen" => _gameSettings.Value.TeenSettings,
                "Adult" => _gameSettings.Value.AdultSettings,
                "Senior" => _gameSettings.Value.SeniorSettings,
                _ => _gameSettings.Value.SeniorSettings,
            };
            return gameSettingsForAgeGroup;
        }

        public string IncreaseFeedometer(Dragon dragon)
        {
            if (dragon.Feedometer < SetCareLevelsForAgeGroups(dragon).MaxFeedometerForAgeGroup)
            {
                dragon.Feedometer += SetCareLevelsForAgeGroups(dragon).FeedometerIncrement;

                dragonsMessage = "That was yummy!";

                return dragonsMessage;
            }

            dragonsMessage = "I'm not hungry!";

            return dragonsMessage;
        }

        public string IncreaseHappiness(Dragon dragon)
        {
            if (dragon.Happiness < SetCareLevelsForAgeGroups(dragon).MaxHappinessForAgeGroup)
            {
                dragon.Happiness += SetCareLevelsForAgeGroups(dragon).HappinessIncrement;

                dragonsMessage = "I love you!";

                return dragonsMessage;
            }

            dragonsMessage = "Leave me alone!!!";

            return dragonsMessage;
        }

        public void ProgressLifeSettings(Dragon dragon)
        {
            dragon.Age += 0.1;
            dragon.Feedometer -= SetCareLevelsForAgeGroups(dragon).HungerIncrement;

            if (dragon.Name == null && dragon.Name == "")
            {
                dragon.Happiness -= SetCareLevelsForAgeGroups(dragon).SadnessIncrement * 2;
            }

            dragon.Happiness -= SetCareLevelsForAgeGroups(dragon).SadnessIncrement;
        }
    }
}
