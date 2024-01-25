using MGTConcerts.Models;
using Microsoft.EntityFrameworkCore;

namespace MGTConcerts.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }       

        public DbSet<MusicVenue> Music_Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<MusicVenue>().HasData(
                new MusicVenue { Id = 1, Name = "Τεχνοπολις", Description = "Τεχνόπολη Δήμου Αθηναίων", Location="Κεραμεικός", Capacity=5000 },
                new MusicVenue { Id = 2, Name = "Fuzz", Description = "Music Club", Location="Ταύρος", Capacity=2000},
                new MusicVenue { Id = 3, Name = "Λιπάσματα", Description = "Συναυλιακός Χώρος Δήμου Πειραιά", Location="Πειραιάς", Capacity=2000 });
        }
    }
}
