using MGTConcerts.Models;

namespace MGTConcerts.Repository
{
    public interface IConcertRepository : IRepository<Concert>
    {
        void Update(Concert concert);
        void Save();
    }
}
