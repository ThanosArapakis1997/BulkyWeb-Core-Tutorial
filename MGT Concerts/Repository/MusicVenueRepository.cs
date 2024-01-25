using MGTConcerts.Data;
using MGTConcerts.Models;
using System.Linq.Expressions;

namespace MGTConcerts.Repository
{
    public class MusicVenueRepository : Repository<MusicVenue>, IMusicVenueRepository
    {

        private readonly ApplicationDbContext _db;
        public MusicVenueRepository(ApplicationDbContext db) : base(db) 
        { 
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(MusicVenue venue)
        {
            _db.Music_Venues.Update(venue);
        }
    }
}
