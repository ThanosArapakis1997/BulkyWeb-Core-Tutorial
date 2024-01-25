using BulkyWeb_Core_Tutorial.Models;

namespace BulkyWeb_Core_Tutorial.Repository
{
    public interface IMusicVenueRepository : IRepository<MusicVenue>
    {
        void Update(MusicVenue venue);
        void Save();
    }
}
