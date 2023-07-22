namespace DomainEvent
{
    public class DomainEvent
    {
        public long Id { get; set; }
        public Guid Identigier { get; set; }
        public DateTime RaiseOn { get; set; }
        public DateTime StartedConsumption { get; set; }
        public DateTime FinishedConsumption { get; set; }
        public string Event { get; set; }
        public string Data { get; set; }
        public string Consumers { get; set; }
        public short Priority { get; set; }
    }
}