using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MGTConcerts.Models
{
    public class Preference
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        [JsonIgnore]
        public ApplicationUser? User { get; set; }

        [Required]
        public bool PriceSensitivity { get; set; }

        public double rockPreference{ get; set; }

        public double popPreference { get; set; }

        public double metalPreference {  get; set; }

        public double rapPreference { get; set; }

        public double indiePreference {  get; set; }

        public double electroPreference { get; set; }



    }
}
