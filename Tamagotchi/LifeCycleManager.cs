using Microsoft.Extensions.Options;

namespace Tamagotchi
{
    internal class LifeCycleManager : ILifeCycleManager
    {
        private readonly GameSettings _gameSettings;
        public string dragonsMessage = string.Empty;

        public LifeCycleManager(IOptions<GameSettings> gameSettings)
        {
            _gameSettings = gameSettings.Value;
        }

        public AgeGroupSettings SetCareLevelsForAgeGroups(Dragon dragon)
        {
            var ageGroup = dragon.AgeGroup;

            AgeGroupSettings gameSettingsForAgeGroup = ageGroup switch
            {
                AgeGroup.Baby => _gameSettings.BabySettings,
                AgeGroup.Child => _gameSettings.ChildSettings,
                AgeGroup.Teen => _gameSettings.TeenSettings,
                AgeGroup.Adult => _gameSettings.AdultSettings,
                AgeGroup.Senior => _gameSettings.SeniorSettings,
                _ => _gameSettings.SeniorSettings,
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
            dragon.Age += _gameSettings.AgeIncrement;
            dragon.Feedometer -= SetCareLevelsForAgeGroups(dragon).HungerIncrement;

            if (dragon.Name == null && dragon.Name == "")
            {
                dragon.Happiness -= SetCareLevelsForAgeGroups(dragon).SadnessIncrement * _gameSettings.NameNeglectPenalty;
            }

            dragon.Happiness -= SetCareLevelsForAgeGroups(dragon).SadnessIncrement;
        }
    }
}
