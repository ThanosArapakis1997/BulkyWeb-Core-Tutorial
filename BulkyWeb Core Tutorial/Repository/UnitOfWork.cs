using MGTConcerts.Data;

namespace MGTConcerts.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IMusicVenueRepository MusicVenue { get; private set; }

        public IArtistRepository Artist { get; private set; }
        
        public IConcertRepository Concert { get; private set; }
        //public IShoppingCartRepository ShoppingCart { get; private set; }
        //public IApplicationUserRepository ApplicationUser { get; private set; }
        //public IOrderHeaderRepository OrderHeader { get; private set; }
        //public IOrderDetailRepository OrderDetail { get; private set; }
        //public IProductImageRepository ProductImage { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            MusicVenue = new MusicVenueRepository(_db);
            Artist = new ArtistRepository(_db);
            Concert = new ConcertRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
