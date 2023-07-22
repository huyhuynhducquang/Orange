namespace DomainEvent
{
    internal class DomainEventConsumer
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public string Consumer { get; set; }
        public string Failure { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public string Notes { get; set; }
    }
}
