using MGTConcerts.Models;

namespace MGTConcerts.Repository
{
    public interface IArtistRepository : IRepository<Artist>
    {
        void Update(Artist artist);
        void AddConcert(Concert concert);

        void RemoveConcert(Concert concert);
        void Save();
    }
}
