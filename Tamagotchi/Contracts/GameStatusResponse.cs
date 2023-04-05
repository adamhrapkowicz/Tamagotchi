using TamagotchiData.Models;

namespace Tamagotchi.Contracts;

public class GameStatusResponse
{
    public bool Success { get; set; }

    public string DragonName { get; set; } = default!;

    public decimal Age { get; set; }

    public AgeGroup AgeGroup { get; set; }

    public bool IsAlive { get; set; }

    public int Feedometer { get; set; }

    public int Happiness { get; set; }

    public GetGameStatusFailureReason? Reason { get; set; }
}

public enum GetGameStatusFailureReason
{
    Dead
}