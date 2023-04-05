namespace Tamagotchi;

public sealed class GameSettings
{
    public int InitialFeedometer { get; set; }

    public int InitialHappiness { get; set; }

    public int MinValueOfFeedometer { get; set; }

    public int MinValueOfHappiness { get; set; }

    public decimal MaxAge { get; set; }

    public double GameStatusTimerInterval { get; set; }

    public double LifeProgressTimerInterval { get; set; }

    public decimal AgeIncrement { get; set; }

    public int NameNeglectPenalty { get; set; }

    public AgeGroupSettings BabySettings { get; set; } = default!;

    public AgeGroupSettings ChildSettings { get; set; } = default!;

    public AgeGroupSettings TeenSettings { get; set; } = default!;

    public AgeGroupSettings AdultSettings { get; set; } = default!;

    public AgeGroupSettings SeniorSettings { get; set; } = default!;
}