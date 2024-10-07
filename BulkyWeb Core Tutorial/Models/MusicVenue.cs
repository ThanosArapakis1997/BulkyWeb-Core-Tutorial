using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace MGTConcerts.Models
{
/*    public enum Location
    {
        Chalandri,
        Ilioupoli,
        Rafina
    }*/

    public class MusicVenue
    {        
        Random random = new Random();
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set;  }

        private int? longitude;
        public int? Longitude {
            get { return longitude; }
            set { longitude = random.Next(0, 101); }
        }

        private int ? latitude;
        public int? Latitude { 
            get { return latitude; }
            set { latitude = random.Next(0, 101); } 
        }

        public int Capacity { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }
        [NotMapped]
        public string? AvailablePeriod
        {
            get
            {
                if (AvailableFrom != null && AvailableTo != null)
                {
                    return AvailableFrom.Value.Day.ToString() + "/" +
                           AvailableFrom.Value.Month.ToString() + " - " + AvailableTo.Value.Day.ToString() + "/" +
                           AvailableTo.Value.Month.ToString();
                }
                return null;
            }
        }
    }
}

