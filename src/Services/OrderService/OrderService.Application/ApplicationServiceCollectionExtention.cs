
using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.Cqrs;
using System.Reflection;

namespace OrderService.Application
{
    public static class ApplicationServiceCollectionExtention
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            services.AddCqrs(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
