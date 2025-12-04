namespace fbiController.DTO
{
    public class FbiListResponse
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public List<FbiItem> Items { get; set; } = new();
    }

    public class FbiItem
    {
        public string Uid { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Sex { get; set; }
        public string? Nationality { get; set; }
        public string? Status { get; set; }
        public string? Url { get; set; }
        public DateTime? Publication { get; set; }
        public List<FbiImage> Images { get; set; } = new();
    }

    public class FbiImage
    {
        public string? Large { get; set; }
        public string? Thumb { get; set; }
        public string? Original { get; set; }
    }
}