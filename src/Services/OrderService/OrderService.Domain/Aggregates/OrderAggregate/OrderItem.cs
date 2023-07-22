using OrderService.Domain.Exceptions;
using OrderService.Domain.SeedWork;

namespace OrderService.Domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity
    {
        // DDD Patterns comment
        // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
        private string _productName;
        private string _pictureUrl;
        private decimal _unitPrice;
        private decimal _discount;
        private int _units;

        public int ProductId { get; private set; }
        protected OrderItem() { }

        public OrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pidctureUrl, int units = 1)
        {
            if (units <= 0)
            {
                throw new OrderServiceDomainException("Invalid number of units");
            }

            if ((unitPrice*units) < discount)
            {
                throw new OrderServiceDomainException("The total of order item is lower than applied discount");
            }

            ProductId = productId;

            _productName = productName;
            _unitPrice = unitPrice;
            _discount = discount;
            _units = units;
            _pictureUrl = pidctureUrl;
        }

        public decimal GetCurrentDiscount()
        {
            return _discount;
        }

        public void SetNewDiscount(decimal discount)
        {
            if (discount < 0)
            {
                throw new OrderServiceDomainException("Discount is not valid");
            }

            _discount = discount;
        }

        public void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new OrderServiceDomainException("Invalid units");
            }

            _units = units;
        }
    }
}
