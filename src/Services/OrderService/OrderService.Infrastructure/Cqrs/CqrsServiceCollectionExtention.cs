using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.Cqrs.Commands;
using OrderService.Infrastructure.Cqrs.Queries;
using System.Reflection;

namespace OrderService.Infrastructure.Cqrs
{
    public static class CqrsServiceCollectionExtention
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(assembly);
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();

            return services;
        }
    }
}
