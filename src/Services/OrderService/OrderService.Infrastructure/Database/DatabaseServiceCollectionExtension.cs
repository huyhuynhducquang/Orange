using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.SeedWork;
using OrderService.Domain.Aggregates.BuyerAggregate;
using OrderService.Domain.Aggregates.OrderAggregate;
using OrderService.Infrastructure.Database.Repositories;

namespace OrderService.Infrastructure.Database
{
    public static class DatabaseServiceCollectionExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderServiceContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("OrderServiceConnectionString"));
            });

            services.AddScoped<IUnitOfWork>(scope => new UnitOfWork(scope.GetService<OrderServiceContext>(), scope.GetService<IMediator>()));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IBuyerRepository, BuyerRepository>();

            return services;
        }
    }
}
