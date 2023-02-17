namespace Tamagotchi
{
    internal sealed class GameSettings
    {
        public int Feedometer { get; set; }

        public int Happiness { get; set; }

        public int MinValueOfFeedometer { get; set; }

        public int MinValueOfHappiness { get; set; }

        public double MaxAge { get; set; }

        public double GameStatusTimerInterval { get; set; }

        public double LifeProgressTimerInterval { get; set; }

        public AgeGroupSettings BabySettings { get; set; } = default!;

        public AgeGroupSettings ChildSettings { get; set; } = default!;

        public AgeGroupSettings TeenSettings { get; set; } = default!;

        public AgeGroupSettings AdultSettings { get; set; } = default!;

        public AgeGroupSettings SeniorSettings { get; set; } = default!;
    }

    public sealed class AgeGroupSettings 
    {
        public int FeedometerIncrement { get; set; }

        public int HappinessIncrement { get; set; }

        public int HungerIncrement { get; set; }

        public int SadnessIncrement { get; set; }

        public int MaxFeedometerForAgeGroup { get; set; }

        public int MaxHappinessForAgeGroup { get; set; }
    }


}
