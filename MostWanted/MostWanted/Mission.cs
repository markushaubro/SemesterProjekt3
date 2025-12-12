namespace MostWanted.Api.Models
{
    public class Mission
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public MissionDecision Decision { get; set; } = MissionDecision.None;

        public Mission(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
