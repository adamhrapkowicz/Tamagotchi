using TamagotchiData.Models;

namespace Tamagotchi.Contracts
{
    public class GameStatusResponse
    {
        public bool Success { get; set; }

        public Dragon StatusDragon { get; set; } = default!;

        public GetGameStatusFailureReason? Reason { get; set; } = default!;
    }

    public enum GetGameStatusFailureReason
    {
        Dead
    }
}
