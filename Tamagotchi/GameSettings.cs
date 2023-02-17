namespace Tamagotchi
{
    public sealed class GameSettings
    {
        public int InitialFeedometer { get; set; }

        public int InitialHappiness { get; set; }

        public int MinValueOfFeedometer { get; set; }

        public int MinValueOfHappiness { get; set; }

        public double MaxAge { get; set; }

        public double GameStatusTimerInterval { get; set; }

        public double LifeProgressTimerInterval { get; set; }

        public double AgeIncrement { get; set; }

        public int NameNeglectPenalty { get; set; }

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
