using OrderService.Domain.Aggregates.BuyerAggregate;

namespace OrderService.Infrastructure.Database.Repositories
{
    internal class BuyerRepository : BaseRepository<Buyer>, IBuyerRepository
    {
        public BuyerRepository(OrderServiceContext context) : base(context)
        {
        }
    }
}
