namespace Tamagotchi
{
    internal class LifeCycleManager : ILifeCycleManager
    {
        public Dictionary<string, int> SetInitialDragonsValues()
        {
            return new Dictionary<string, int> { { "Feedometer", 10 }, { "Happiness", 50 } };
        }

        public string dragonsmessage = string.Empty;

        public string IncreaseFeedometer(Dragon dragon)
        {
            if (dragon.Feedometer < CareLevelManager(dragon)["maxFeedometerForAgeGroup"])
            {
                dragon.Feedometer += CareLevelManager(dragon)["feedometerIncrement"];

                dragonsmessage = "That was yummy!";

                return dragonsmessage;
            }

            dragonsmessage = "I'm not hungry!";

            return dragonsmessage;
        }

        public string IncreaseHappiness(Dragon dragon)
        {
            if (dragon.Happiness < CareLevelManager(dragon)["maxHappinessForAgeGroup"])
            {
                dragon.Happiness += CareLevelManager(dragon)["happinessIncrement"];

                dragonsmessage = "I love you!";

                return dragonsmessage;
            }

            dragonsmessage = "Leave me alone!!!";

            return dragonsmessage;
        }

        public void ProgressLifeSettings(Dragon dragon)
        {
            dragon.Age += 0.1;
            dragon.Feedometer -= CareLevelManager(dragon)["hungerIncrement"];

            if (dragon.Name == null && dragon.Name == "")
            {
                dragon.Happiness -= CareLevelManager(dragon)["sadnessIncrement"] * 2;
            }

            dragon.Happiness -= CareLevelManager(dragon)["sadnessIncrement"];
        }

        public Dictionary<string, int> CareLevelManager(Dragon dragon)
        {
            var ageGroup = dragon.AgeGroup.ToString();

            int feedometerIncrement, happinessIncrement, hungerIncrement, sadnessIncrement, maxFeedometerForAgeGroup, maxHappinessForAgeGroup;

            switch (ageGroup)
            {
                case "Baby":
                    feedometerIncrement = 10;
                    happinessIncrement = 15;
                    hungerIncrement  = 3;
                    sadnessIncrement = 4;
                    maxFeedometerForAgeGroup = 30;
                    maxHappinessForAgeGroup = 200;
                    break;

                case "Child":
                    feedometerIncrement = 15;
                    happinessIncrement = 20;
                    hungerIncrement  = 2;
                    sadnessIncrement = 3;
                    maxFeedometerForAgeGroup = 80;
                    maxHappinessForAgeGroup = 150;
                    break;

                case "Teen":
                    feedometerIncrement = 25;
                    happinessIncrement = 35;
                    hungerIncrement  = 5;
                    sadnessIncrement = 1;
                    maxFeedometerForAgeGroup = 150;
                    maxHappinessForAgeGroup = 50;
                    break;

                case "Adult":
                    feedometerIncrement= 20;
                    happinessIncrement = 25;
                    hungerIncrement  = 2;
                    sadnessIncrement = 2;
                    maxFeedometerForAgeGroup = 100;
                    maxHappinessForAgeGroup = 100;
                    break;

                case "Senior":
                    feedometerIncrement = 15;
                    happinessIncrement = 30;
                    hungerIncrement  = 1;
                    sadnessIncrement = 1;
                    maxFeedometerForAgeGroup = 90;
                    maxHappinessForAgeGroup = 100;
                    break;

                default:
                    feedometerIncrement = 10;
                    happinessIncrement = 15;
                    hungerIncrement  = 1;
                    sadnessIncrement = 1;
                    maxFeedometerForAgeGroup = 100;
                    maxHappinessForAgeGroup = 100;
                    break;

            }

            return new Dictionary<string, int> {
                { "feedometerIncrement", feedometerIncrement },
                { "happinessIncrement", happinessIncrement },
                { "hungerIncrement", hungerIncrement },
                { "sadnessIncrement", sadnessIncrement },
                { "maxFeedometerForAgeGroup", maxFeedometerForAgeGroup },
                { "maxHappinessForAgeGroup", maxHappinessForAgeGroup }
            };
        }
    }
}
