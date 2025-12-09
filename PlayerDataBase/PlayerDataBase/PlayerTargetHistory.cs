using System.Text.Json.Serialization;

public class PlayerTargetHistory
{
    public int Id { get; set; }

    public int PlayerScoreboardId { get; set; }

    public string TargetName { get; set; } = "";
    public int TimesShot { get; set; }

    [JsonIgnore]
    public PlayerScoreboard Player { get; set; } = null!;
}
