using MediatR;

namespace OrderService.Infrastructure.Cqrs.Commands
{
    public interface IDomainEventConsumer<TNotification> : INotificationHandler<TNotification>
        where TNotification : INotification
    {
    }
}
