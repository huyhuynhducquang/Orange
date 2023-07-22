using MediatR;
using OrderService.Domain.SeedWork;
using OrderService.Infrastructure.Database;

namespace OrderService.Infrastructure.Domain
{
    internal static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, OrderServiceContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Aggregate>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
