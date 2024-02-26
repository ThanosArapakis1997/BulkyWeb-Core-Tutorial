using MGTConcerts.Models;

namespace MGTConcerts.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order order);
        void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);

        void Save();
    }
}
