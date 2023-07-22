using OrderService.Domain.Aggregates.OrderAggregate;

namespace OrderService.Infrastructure.Database.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderServiceContext context) : base(context)
        {
        }
    }
}
