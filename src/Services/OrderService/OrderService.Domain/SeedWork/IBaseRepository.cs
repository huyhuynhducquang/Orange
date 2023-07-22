namespace OrderService.Domain.SeedWork
{
    public interface IBaseRepository<TAggregate> 
        where TAggregate : Aggregate
    {
        Task CreateAsync(TAggregate aggreate, CancellationToken cancellationToken);
        void Delete(TAggregate aggreate);
        void Update(TAggregate aggreate);
    }
}
