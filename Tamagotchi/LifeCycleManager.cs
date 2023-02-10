namespace Tamagotchi
{
    internal class LifeCycleManager : ILifeCycleManager
    {
        public string IncreaseFeedometer(Dragon dragon)
        {
            dragon.Feedometer += CareLevelManager(dragon)[0];

            var dragonsmessage = "That was yummy!";

            return dragonsmessage;
        }

        public string IncreaseHappiness(Dragon dragon)
        {
            dragon.Happiness += CareLevelManager(dragon)[1];

            var dragonsmessage = "I love you!";

            return dragonsmessage;
        }

        public void ProgressLifeSettings(Dragon dragon)
        {
            dragon.Age += 0.1;
            dragon.Feedometer -= CareLevelManager(dragon)[2];
            dragon.Happiness -= CareLevelManager(dragon)[3];
        }

        public int[] CareLevelManager(Dragon dragon)
        {
            var ageGroup = dragon.AgeGroup.ToString();

            int feedometerIncrement, happinessIncrement, hungerIncrement, sadnessIncrement;

            switch (ageGroup)
            {
                case "Baby":
                    feedometerIncrement = 10;
                    happinessIncrement = 15;
                    hungerIncrement  = 3;
                    sadnessIncrement = 4;
                    break;

                case "Child":
                    feedometerIncrement = 15;
                    happinessIncrement = 20;
                    hungerIncrement  = 2;
                    sadnessIncrement = 3;
                    break;

                case "Teen":
                    feedometerIncrement = 25;
                    happinessIncrement = 35;
                    hungerIncrement  = 5;
                    sadnessIncrement = 1;
                    break;

                case "Adult":
                    feedometerIncrement= 20;
                    happinessIncrement = 25;
                    hungerIncrement  = 2;
                    sadnessIncrement = 2;
                    break;

                case "Senior":
                    feedometerIncrement = 15;
                    happinessIncrement = 30;
                    hungerIncrement  = 1;
                    sadnessIncrement = 1;
                    break;

                default:
                    feedometerIncrement = 10;
                    happinessIncrement = 15;
                    hungerIncrement  = 1;
                    sadnessIncrement = 1;
                    break;

            }

            return new[] { feedometerIncrement, happinessIncrement, hungerIncrement, sadnessIncrement };
        }
    }
}
