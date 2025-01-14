﻿using MGTConcerts.Data;

namespace MGTConcerts.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IMusicVenueRepository MusicVenue { get; private set; }

        public IArtistRepository Artist { get; private set; }
        
        public IConcertRepository Concert { get; private set; }
        public IOrderRepository Order { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }  
        
        public IPreferenceRepository Preference { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            MusicVenue = new MusicVenueRepository(_db);
            Artist = new ArtistRepository(_db);
            Concert = new ConcertRepository(_db);
            Order = new OrderRepository(_db);
            Preference = new PreferenceRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
