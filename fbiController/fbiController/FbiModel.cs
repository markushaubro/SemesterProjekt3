namespace fbiController.Models
{
    public class WantedPerson
    {
        public int Id { get; set; }
        public string FbiUid { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Sex { get; set; }
        public string? Nationality { get; set; }
        public string? Status { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? Publication { get; set; }

        public DateTime LastSyncedUtc { get; set; }
    }
}
