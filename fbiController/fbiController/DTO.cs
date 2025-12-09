using System.Text.Json.Serialization;

namespace fbiController.DTO
{
    public class FbiListResponse
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("items")]
        public List<FbiItem> Items { get; set; } = new();
    }

    public class FbiItem
    {
        [JsonPropertyName("uid")]
        public string Uid { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("sex")]
        public string? Sex { get; set; }

        [JsonPropertyName("nationality")]
        public string? Nationality { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("publication")]
        public DateTime? Publication { get; set; }

        [JsonPropertyName("images")]
        public List<FbiImage>? Images { get; set; }

        [JsonPropertyName("reward_min")]
        public decimal? RewardMin { get; set; }

        [JsonPropertyName("reward_max")]
        public decimal? RewardMax { get; set; }

    }

    public class FbiImage
    {
        [JsonPropertyName("thumb")]
        public string? Thumb { get; set; }

        [JsonPropertyName("large")]
        public string? Large { get; set; }
    }
}
