using MGTConcerts.Models;

namespace MGTConcerts.Repository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser user);
        void Save();
    }
}