using System.Collections.Generic;
using MostWanted.Api.Models;

namespace MostWanted.Api.Data
{
    public static class OpponentRepository
    {
        public static List<Opponent> GetOpponents()
        {
            return new List<Opponent>
            {
                new Opponent(1, "Alpha",   OpponentIcon.Skull,  "#FF4A4A"),
                new Opponent(2, "Bravo",   OpponentIcon.Shield, "#4A90E2"),
                new Opponent(3, "Charlie", OpponentIcon.Star,   "#F5A623"),
                new Opponent(4, "Delta",   OpponentIcon.Target, "#7ED321")
            };
        }
    }
}
