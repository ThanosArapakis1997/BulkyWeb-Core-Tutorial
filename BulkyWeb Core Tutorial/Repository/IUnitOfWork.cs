namespace MGTConcerts.Repository
{
    public interface IUnitOfWork
    {
        IMusicVenueRepository MusicVenue { get; }

        void Save();
    }
}
