using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileLib
{
    public class Villain
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public int MaxReward { get; set; } = 100;

        public bool IsActive { get; set; } = true;
        public int? CaughtByUserId { get; set; }

        [ForeignKey("CaughtByUserId")]
        public CurrentUser? CaughtByUser { get; set; }
    }
}
