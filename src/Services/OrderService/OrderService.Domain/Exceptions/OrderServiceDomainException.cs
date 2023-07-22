namespace OrderService.Domain.Exceptions
{
    public class OrderServiceDomainException : Exception
    {
        public OrderServiceDomainException() { }

        public OrderServiceDomainException(string message) : base(message) { }

        public OrderServiceDomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
