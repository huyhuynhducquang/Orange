using Microsoft.EntityFrameworkCore;
using OrderService.Domain.SeedWork;

namespace OrderService.Infrastructure.Database.Repositories
{
    public class BaseRepository<TAggregate> : IBaseRepository<TAggregate>
        where TAggregate : Aggregate
    {
        protected DbContext DbContext { get; }

        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task CreateAsync(TAggregate aggreate, CancellationToken cancellationToken)
        {
            DbContext.Set<TAggregate>().AddAsync(aggreate, cancellationToken);
            return Task.CompletedTask;
        }

        public void Delete(TAggregate aggreate)
        {
            DbContext.Set<TAggregate>().Remove(aggreate);
        }

        public void Update(TAggregate aggreate)
        {
            DbContext.Set<TAggregate>().Update(aggreate);
        }
    }
}
