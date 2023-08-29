using EventBus;

namespace OrderService.Application.IntergrationEvents.Events
{
    public record OrderStartedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; init; }

        public OrderStartedIntegrationEvent(string userId)
            => UserId = userId;
    }
}
