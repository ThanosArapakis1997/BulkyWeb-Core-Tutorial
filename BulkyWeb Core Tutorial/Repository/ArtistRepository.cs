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