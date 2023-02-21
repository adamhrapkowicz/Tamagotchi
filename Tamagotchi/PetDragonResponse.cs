namespace Tamagotchi
{
    public class PetDragonResponse
    {
        public bool Success { get; set; }

        public PettingFailureReason? Reason { get; set; } = default!;
    }

    public enum PettingFailureReason
    {
        Dead,
        Overpetted
    }
}