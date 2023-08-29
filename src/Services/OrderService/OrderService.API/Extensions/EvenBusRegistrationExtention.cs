namespace OrderService.Extensions
{
    public static class EvenBusRegistrationExtention
    {
        public static IServiceCollection AddEventBusEventHandlerCollection(this IServiceCollection collection)
        {
            collection.AddTransient<IIntegrationEventHandler<OrderStartedIntegrationEvent>, OrderStartedIntegrationEventHandler>();
            return collection;
        }

        public static IServiceProvider AddEventBusSubcribes(this IServiceProvider provider)
        {
            var eventBus = provider.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderStartedIntegrationEvent, IIntegrationEventHandler<OrderStartedIntegrationEvent>>();
            return provider;
        }
    }
}
