using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace MGTConcerts.Models
{
    [NotMapped]
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public int ?Name { get; set; }

        public string? Address {  get; set; }
        public string? Phone {  get; set; }
    }
}
