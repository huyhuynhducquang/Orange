using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OrderService.Infrastructure.Database;
using OrderService.Infrastructure.Cqrs;
using System.Reflection;

namespace OrderService.Infrastructure
{
    public static class InfrastructureServiceCollectionExtention
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            return services;
        }
    }
}
