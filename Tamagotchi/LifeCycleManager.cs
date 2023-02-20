using Microsoft.Extensions.Options;

namespace Tamagotchi
{
    internal class LifeCycleManager : ILifeCycleManager
    {
        private readonly GameSettings _gameSettings;
        private readonly DragonMessages _dragonMessages;
        public string dragonsMessage = string.Empty;

        public LifeCycleManager(IOptions<GameSettings> gameSettings, IOptions<DragonMessages> dragonMessages)
        {
            _gameSettings = gameSettings.Value;
            _dragonMessages = dragonMessages.Value;
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

                dragonsMessage = _dragonMessages.FeedingSuccess;

                return dragonsMessage;
            }

            dragonsMessage = _dragonMessages.Overfeeding;

            return dragonsMessage;
        }

        public string IncreaseHappiness(Dragon dragon)
        {
            if (dragon.Happiness < SetCareLevelsForAgeGroups(dragon).MaxHappinessForAgeGroup)
            {
                dragon.Happiness += SetCareLevelsForAgeGroups(dragon).HappinessIncrement;

                dragonsMessage = _dragonMessages.PettingSuccess;

                return dragonsMessage;
            }

            dragonsMessage = _dragonMessages.Overpetting;

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
