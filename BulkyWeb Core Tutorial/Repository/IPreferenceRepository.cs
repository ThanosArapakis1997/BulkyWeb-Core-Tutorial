using MGTConcerts.Models;

namespace MGTConcerts.Repository
{
    public interface IPreferenceRepository : IRepository<Preference>
    {
        void Update(Preference preference);
        void Save();
    }
}
