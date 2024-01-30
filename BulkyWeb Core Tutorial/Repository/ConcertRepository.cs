using MGTConcerts.Data;
using MGTConcerts.Models;
using System.Linq.Expressions;

namespace MGTConcerts.Repository
{
    public class ConcertRepository : Repository<Concert>, IConcertRepository
    {
        private readonly ApplicationDbContext _db;
        public ConcertRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }        

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Concert concert)
        {
            _db.Update(concert);
        }
    }
}
