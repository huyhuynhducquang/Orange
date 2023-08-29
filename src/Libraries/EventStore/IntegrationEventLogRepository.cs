using EventBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Reflection;

namespace EventStore
{
    internal class IntegrationEventLogService : IIntegrationEventLogService
    {
        private readonly DbConnection dbConnection;
        private readonly IntegrationEventStoreContext eventStoreContext;
        private readonly List<Type> eventTypes;

        public IntegrationEventLogService(DbConnection dbConnection)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            eventStoreContext = new IntegrationEventStoreContext(
                new DbContextOptionsBuilder<IntegrationEventStoreContext>()
                    .UseSqlServer(dbConnection)
                    .Options);

            eventTypes = Assembly.Load(Assembly.GetEntryAssembly().FullName)
                .GetTypes()
                .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
                .ToList();
        }

        public Task MarkEventAsFailedAsync(Guid eventId)
        {
            return UpdateEventsStatus(eventId, EventState.Failed);
        }

        public Task MarkEventAsInProgressAsync(Guid eventId)
        {
            return UpdateEventsStatus(eventId, EventState.InProgress);
        }

        public Task MarkEventAsPublishedAsync(Guid eventId)
        {
            return UpdateEventsStatus(eventId, EventState.Published);
        }

        public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            var eventLog = new IntegrationEventLog(@event, transaction.TransactionId);

            eventStoreContext.Database.UseTransaction(transaction.GetDbTransaction());
            eventStoreContext.IntegrationEventLogs.Add(eventLog);

            return eventStoreContext.SaveChangesAsync();
        }

        private Task UpdateEventsStatus(Guid guid, EventState eventState)
        {
            var eventLog = eventStoreContext.IntegrationEventLogs.Single(_ => _.GuidId == guid);
            eventLog.State = eventState;

            if (eventState == EventState.InProgress)
                eventLog.TimesSent++;

            eventStoreContext.IntegrationEventLogs.Update(eventLog);
            return eventStoreContext.SaveChangesAsync();
        }

    }
}
