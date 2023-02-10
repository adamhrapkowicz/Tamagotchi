namespace Tamagotchi
{
    internal class LifeCycleManager : ILifeCycleManager
    {
        public string IncreaseFeedometer(Dragon dragon)
        {
            dragon.Feedometer += FeedometerIncrement(dragon);

            var dragonsmessage = "That was yummy!";

            return dragonsmessage;
        }

        private static int FeedometerIncrement(Dragon dragon)
        {
            var ageGroup = dragon.AgeGroup.ToString();

            int feedingIncrement;

            switch (ageGroup)
            {
                case "Baby":
                    feedingIncrement = 10;
                    break;

                case "Child":
                    feedingIncrement = 15;
                    break;

                case "Teen":
                    feedingIncrement = 25;
                    break;

                case "Adult":
                    feedingIncrement= 20;
                    break;

                case "Senior":
                    feedingIncrement = 15;
                    break;

                default:
                    feedingIncrement = 10;
                    break;

            }

            return feedingIncrement;
        }

        public string IncreaseHappiness(Dragon dragon)
        {
            dragon.Happiness += 50;

            var dragonsmessage = "I love you!";

            return dragonsmessage;
        }

        public void ProgressLifeSettings(Dragon dragon)
        {
            dragon.Age += 0.1;
            dragon.Feedometer--;
            dragon.Happiness--;
        }
    }
}
