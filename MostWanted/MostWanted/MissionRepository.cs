using System.Collections.Generic;
using MostWanted.Api.Models;

namespace MostWanted.Api.Data
{
    public static class MissionRepository
    {
        public static List<Mission> GetMissions()
        {
            return new List<Mission>
            {
                new Mission("Mission 1", "Find og eliminér dit første target."),
                new Mission("Mission 2", "Bevæg dig tættere på et nyt target og eliminér det."),
                new Mission("Mission 3", "Eliminér et target som er længst væk.")
            };
        }
    }
}
