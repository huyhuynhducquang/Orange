using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStore
{
    internal class IntegrationEventStoreContext : DbContext
    {
        public IntegrationEventStoreContext(DbContextOptions<IntegrationEventStoreContext> options) : base(options)
        {
        }

        public DbSet<IntegrationEventLog> IntegrationEventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntegrationEventLog>(ConfigureIntegrationEventLog);
        }

        private void ConfigureIntegrationEventLog(EntityTypeBuilder<IntegrationEventLog> builder)
        {
            builder.ToTable("IntegrationEventLog");

            builder.HasKey(e => e.GuidId);

            builder.Property(e => e.GuidId)
                .IsRequired();

            builder.Property(e => e.Data)
                .IsRequired();

            builder.Property(e => e.CreatedTime)
                .IsRequired();

            builder.Property(e => e.State)
                .IsRequired();

            builder.Property(e => e.EventName)
                .IsRequired();
        }
    }
}
