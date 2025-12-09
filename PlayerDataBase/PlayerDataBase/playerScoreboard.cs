public class PlayerScoreboard
{
    public int Id { get; set; }
    public string PlayerName { get; set; } = "";
    public int TotalBountyCollected { get; set; }
    public int TargetsTakenOut { get; set; }
    public int HighestBountyCollected { get; set; }

    public int TotalShots { get; set; }
    public string MostShotTarget { get; set; } = "";
    public int TotalGames { get; set; }

    public List<PlayerTargetHistory> TargetHistory { get; set; } = new();
}
