using MediatR;

namespace OrderService.Infrastructure.Domain
{
    public interface IDomainEvent : INotification
    {
    }
}
