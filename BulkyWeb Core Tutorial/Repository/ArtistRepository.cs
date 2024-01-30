using MGTConcerts.Data;
using MGTConcerts.Models;
using System.Linq;
using System.Linq.Expressions;

namespace MGTConcerts.Repository
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        private readonly ApplicationDbContext _db;
        public ArtistRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void AddConcert(Concert concert)
        {
            if (concert == null)
            {
                Artist artist = _db.Artists.FirstOrDefault(u => u.Id == concert.ArtistId);
                if(artist != null) 
                {
                    artist.Concerts.Add(concert);
                    _db.SaveChanges();
                }
            }

        }

        public void RemoveConcert(Concert concert)
        {
            if (concert == null)
            {
                Artist artist = _db.Artists.FirstOrDefault(u => u.Id == concert.ArtistId);
                if (artist != null)
                {
                    artist.Concerts.Remove(concert);
                    _db.SaveChanges();
                }
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Artist artist)
        {
            _db.Artists.Update(artist);
        }
    }
}
