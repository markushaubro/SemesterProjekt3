namespace fbiController.Models
{
    public class WantedPerson
    {
        public int Id { get; set; }

        public string FbiUid { get; set; } = string.Empty;

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Sex { get; set; }
        public string? Nationality { get; set; }
        public string? Status { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? Publication { get; set; }
        public DateTime LastSyncedUtc { get; set; }

        public decimal? RewardMin { get; set; }
        public decimal? RewardMax { get; set; }
    }
}
