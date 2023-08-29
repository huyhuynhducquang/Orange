using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Aggregates.BuyerAggregate;
using OrderService.Domain.Aggregates.OrderAggregate;
using OrderService.Infrastructure.Database.EntityConfigurations;

namespace OrderService.Infrastructure.Database
{
    public class OrderServiceContext : DbContext
    {

        public const string DEFAULT_SCHEMA = "OrderService";
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }

        public OrderServiceContext(DbContextOptions<OrderServiceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // DDD Patterns comment
            // An important capability of EF is the ability to use POCO domain entities. Your domain model classes are persistence-ignorant.
            // To mapping domain model againsts database I use Fluent APi instead of data annotations in EF.
            modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CardTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
        }
    }
}
