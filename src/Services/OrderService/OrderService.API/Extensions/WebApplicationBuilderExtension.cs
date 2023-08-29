using EventBus;
using EventBus.Abstractions;
using EventBus.RabbitMQ;
using RabbitMQ.Client;

namespace OrderService.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static WebApplicationBuilder AddMicroserviceRegistration(this WebApplicationBuilder builder)
        {
            builder.Services.AddEventBus(builder.Configuration);

            return builder;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusSection = configuration.GetSection("EventBus");
            if (!eventBusSection.Exists())
            {
                return services;
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = configuration.GetConnectionString("EventBus"),
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(eventBusSection["UserName"]))
                {
                    connectionFactory.UserName = eventBusSection["UserName"];
                }

                if (!string.IsNullOrEmpty(eventBusSection["Password"]))
                {
                    connectionFactory.Password = eventBusSection["Password"];
                }

                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                return new DefaultRabbitMQPersistentConnection(connectionFactory, logger);
            });
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                var rabbitMQPersistentConnection = serviceProvider.GetRequiredService<IRabbitMQPersistentConnection>();
                var eventBusSubscriptionsManager = serviceProvider.GetRequiredService<IEventBusSubscriptionsManager>();
                var logger = serviceProvider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var subscriptionClientName = eventBusSection.GetValue<string>("SubscriptionClientName");
                var retryCount = eventBusSection.GetValue("RetryCount", 5);

                return new EventBusRabbitMQ(
                    rabbitMQPersistentConnection,
                    eventBusSubscriptionsManager,
                    serviceProvider,
                    logger,
                    subscriptionClientName,
                    retryCount
                    );
            });


            return services;
        }

    }
}
