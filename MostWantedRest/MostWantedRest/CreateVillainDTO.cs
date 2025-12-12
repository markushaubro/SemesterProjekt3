using System.ComponentModel.DataAnnotations;

namespace MostWantedRest
{
    public class CreateVillainDTO
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public int MaxReward { get; set; }
    }
}
