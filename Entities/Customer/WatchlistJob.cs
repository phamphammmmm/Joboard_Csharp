namespace Joboard.Entities.Customer
{
    public class WatchlistJob : Activity
    {
        public int UserId { get; set; }
        public required User User { get; set; }

        public int JobId { get; set; }
        public required Job Job { get; set; }
    }
}
