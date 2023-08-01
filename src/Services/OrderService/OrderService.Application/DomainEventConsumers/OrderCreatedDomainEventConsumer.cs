using OrderService.Domain.Aggregates.Events;
using OrderService.Infrastructure.Cqrs.Commands;

namespace OrderService.Application.DomainEventConsumers
{
    public class OrderCreatedDomainEventConsumer : IDomainEventConsumer<OrderCreatedDomainEvent>
    {
        public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
