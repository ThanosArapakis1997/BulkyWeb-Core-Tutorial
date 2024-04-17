using MGTConcerts.Models;

namespace MGTConcerts.Repository
{
    public interface IUnitOfWork
    {
        IMusicVenueRepository MusicVenue { get; }
        IArtistRepository Artist { get; }
        IConcertRepository Concert { get; }
        IOrderRepository Order { get; }

        IApplicationUserRepository ApplicationUser { get; } 

        

        void Save();
    }
}
