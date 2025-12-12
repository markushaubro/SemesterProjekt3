namespace MostWanted.Api.Models
{
    public class Opponent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OpponentIcon Icon { get; set; }
        public string ColorHex { get; set; }

        public Opponent(int id, string name, OpponentIcon icon, string colorHex)
        {
            Id = id;
            Name = name;
            Icon = icon;
            ColorHex = colorHex;
        }
    }
}
