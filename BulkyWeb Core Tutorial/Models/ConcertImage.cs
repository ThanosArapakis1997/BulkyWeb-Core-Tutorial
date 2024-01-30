using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MGTConcerts.Models
{
    public class ConcertImage
    {
        public int Id { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        public int ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        public Artist? Artist { get; set; }
    }
}