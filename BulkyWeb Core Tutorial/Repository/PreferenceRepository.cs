using MGTConcerts.Data;
using MGTConcerts.Models;

namespace MGTConcerts.Repository
{
    public class PreferenceRepository : Repository<Preference>, IPreferenceRepository
    {
        private readonly ApplicationDbContext _db;
        public PreferenceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Preference preference)
        {
            _db.Preferences.Update(preference);
        }
    }
}
