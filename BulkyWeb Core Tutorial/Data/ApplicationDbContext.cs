using MGTConcerts.Models;
using Microsoft.EntityFrameworkCore;

namespace MGTConcerts.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }       

        public DbSet<MusicVenue> Music_Venues { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
