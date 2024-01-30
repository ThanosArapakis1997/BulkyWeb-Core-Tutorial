using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MGTConcerts.Models
{
    public class Concert
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        [ValidateNever]
        public Artist? Artist { get; set; }
        public string? ArtistName
        {
            get
            {
                if (Artist != null) return Artist.Name;
                return "";
            }
        }
            

        [Required]
        public DateTime? ConcertDate { get; set; }

        [Required]
        [Range(1, 1000)]
        public double Price { get; set; }

        public int MusicVenueId { get; set; }
        [ForeignKey("MusicVenueId")]
        [ValidateNever]
        public MusicVenue? MusicVenue { get; set; }
        public string? VenueName
        {
            get
            {
                if (MusicVenue != null) return MusicVenue.Name;
                return "";
            }
        }
    }
}
