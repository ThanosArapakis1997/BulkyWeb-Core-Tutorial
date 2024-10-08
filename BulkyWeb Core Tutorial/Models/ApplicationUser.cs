using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;



namespace MGTConcerts.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public required string Name { get; set; }
        
        [NotMapped]
        public string? Role { get; set; }

        public int? Longitude { get; set; }
        public int? Latitude { get; set; }

        public int? PreferenceId { get; set; }
        [ForeignKey("PreferenceId")]
        [ValidateNever]
        public Preference? Preference { get; set; }
    }
}
