using System.ComponentModel.DataAnnotations;

namespace BulkyWeb_Core_Tutorial.Models
{
    public class MusicVenue
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

    }
}
