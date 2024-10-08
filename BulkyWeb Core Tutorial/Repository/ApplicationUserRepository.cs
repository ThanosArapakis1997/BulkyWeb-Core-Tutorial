using MGTConcerts.Data;
using MGTConcerts.Models;
using System.Linq;
using System.Linq.Expressions;

namespace MGTConcerts.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ApplicationUser user)
        {
            _db.ApplicationUsers.Update(user);
        }

    }
}