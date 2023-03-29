using TamagotchiData.Models;

namespace Tamagotchi.Contracts
{
    public class GameStatusResponse
    {
        public bool Success { get; set; }

        //public Dragon StatusDragon { get; set; } = default!;

        public string Name { get; set; } = default!;

        public double Age { get; set; }

        public AgeGroup AgeGroup { get; set; }

        public bool IsAlive { get; set; }

        public int Feedometer { get; set; }

        public int Happiness { get; set; }

        public GetGameStatusFailureReason? Reason { get; set; } = default!;
    }

    public enum GetGameStatusFailureReason
    {
        Dead
    }
}
