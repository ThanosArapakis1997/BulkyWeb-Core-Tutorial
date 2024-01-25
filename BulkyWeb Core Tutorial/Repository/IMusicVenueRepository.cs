using MGTConcerts.Models;

namespace MGTConcerts.Repository
{
    public interface IMusicVenueRepository : IRepository<MusicVenue>
    {
        void Update(MusicVenue venue);
        void Save();
    }
}
