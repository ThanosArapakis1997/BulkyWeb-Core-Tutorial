using MGTConcerts.Data;

namespace MGTConcerts.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IMusicVenueRepository MusicVenue { get; private set; }


        //public ICompanyRepository Company { get; private set; }
        //public IProductRepository Product { get; private set; }
        //public IShoppingCartRepository ShoppingCart { get; private set; }
        //public IApplicationUserRepository ApplicationUser { get; private set; }
        //public IOrderHeaderRepository OrderHeader { get; private set; }
        //public IOrderDetailRepository OrderDetail { get; private set; }
        //public IProductImageRepository ProductImage { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            MusicVenue = new MusicVenueRepository(_db);
            
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
