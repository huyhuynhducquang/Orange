using OrderService.Application.Dtos;
using OrderService.Application.Models;
using OrderService.Infrastructure.Cqrs.Commands;

namespace OrderService.Application.Cqrs.Commands
{
    public class CreateOrderCommand : ICommand<bool>
    {
        private readonly List<OrderItemDto> _orderItems;

        public string UserId { get; }
        public string UserName { get; }
        public string City { get; }
        public string Street { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }
        public string CardNumber { get; }
        public string CardHolderName { get; }
        public DateTime CardExpiration { get; }
        public string CardSecurityNumber { get; }
        public int CardTypeId { get; }

        public IEnumerable<OrderItemDto> OrderItems => _orderItems;

        protected CreateOrderCommand()
        {
            _orderItems = new List<OrderItemDto>();
        }

        //public CreateOrderCommand(List<BasketItem> basketItems, string userId, string userName
        //    , string city, string street, string state, string country, string zipcode
        //    , string cardNumber, string cardHolderName, DateTime cardExpiration
        //    , string cardSercurityNumber, int cardTypeId) : this()
        //{
        //    _orderItems = basketItems.ToOrderItemsDto().ToList();
        //    UserId = userId;
        //    UserName = userName;
        //    City = city;
        //    Street = street;
        //    State = state;
        //    Country = country;
        //    ZipCode = zipcode;
        //    CardNumber = cardNumber;
        //    CardHolderName = cardHolderName;
        //    CardExpiration = cardExpiration;
        //    CardHolderName = cardHolderName;
        //    CardExpiration = cardExpiration;
        //    CardSecurityNumber = cardSercurityNumber;
        //    CardTypeId = cardTypeId;
        //}

        public CreateOrderCommand(List<OrderItemDto> orderItemDtos, string userId, string userName
            , string city, string street, string state, string country, string zipcode
            , string cardNumber, string cardHolderName, DateTime cardExpiration
            , string cardSercurityNumber, int cardTypeId) : this()
        {
            _orderItems = orderItemDtos;
            UserId = userId;
            UserName = userName;
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipcode;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSercurityNumber;
            CardTypeId = cardTypeId;
        }
    }

}
