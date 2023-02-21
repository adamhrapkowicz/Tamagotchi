namespace Tamagotchi
{
    public class FeedDragonResponse
    {
        public bool Success { get; set; }

        public FeedingFailureReason? Reason { get; set; } = default!;
    }

    public enum FeedingFailureReason
    {
        Dead,
        Full
    }
}