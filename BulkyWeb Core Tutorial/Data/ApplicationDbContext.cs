using MGTConcerts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Numerics;

namespace MGTConcerts.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }       

        public DbSet<MusicVenue> Music_Venues { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Preference> Preferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //tin timi tou enum tha tin katalaveineis san string 
            modelBuilder
                        .Entity<Concert>()
                        .Property(p => p.Genre)
                        .HasConversion(
                            v => v.ToString(),
                            v => (Genre)Enum.Parse(typeof(Genre), v));
        }
    }
}
