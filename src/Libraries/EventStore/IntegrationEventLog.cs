using EventBus;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace EventStore
{
    internal class IntegrationEventLog
    {
        private static readonly JsonSerializerOptions s_indentedOptions = new() { WriteIndented = true };
        private static readonly JsonSerializerOptions s_caseInsensitiveOptions = new() { PropertyNameCaseInsensitive = true };

        public Guid GuidId { get; private set; }
        public string EventName { get; private set; }
        [NotMapped]
        public string EventShortName => EventName.Split('.').Last();
        public EventState State { get; set; }
        public DateTime CreatedTime { get; private set; }
        public string Data { get; private set; }
        public int TimesSent { get; set; }
        public string TransactionId { get; private set; }

        private IntegrationEventLog() { }
        public IntegrationEventLog(IntegrationEvent @event, Guid transactionId)
        {
            GuidId = @event.Id;
            EventName = @event.GetType().FullName ?? throw new ArgumentNullException(nameof(EventName));
            State = EventState.NotPublished;
            CreatedTime = @event.CreatedTime;
            Data = JsonSerializer.Serialize(@event, @event.GetType(), s_indentedOptions);
            TransactionId = transactionId.ToString();
        }
    }

    public enum EventState
    {
        NotPublished = 0,
        InProgress = 1,
        Published = 2,
        Failed = 3
    }
}
