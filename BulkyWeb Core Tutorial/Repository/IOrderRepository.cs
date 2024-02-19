using MGTConcerts.Models;

namespace MGTConcerts.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order order);
        void Save();
    }
}
