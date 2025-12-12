using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileLib
{
    public class CurrentUser
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Profile")]
        public int ProfileId { get; set; }

        public bool IsPlaying { get; set; }
        public DateTime GameStartedAt { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        //Bruges til location på den aktive spiller
        public double? Latitude { get; set; } = 55.630767667077436;
        public double? Longitude { get; set; } = 12.078199489762422;
        // Navigation property
        public Profile? Profile { get; set; }
    }
}
