using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MGTConcerts.Models
{
    public class MusicVenue
    {        
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
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

