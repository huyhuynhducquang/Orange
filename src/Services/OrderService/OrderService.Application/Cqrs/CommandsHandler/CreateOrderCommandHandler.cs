using OrderService.Domain.Aggregates.OrderAggregate;
using OrderService.Infrastructure.Cqrs.Commands;

namespace OrderService.Application.Cqrs.CommandsHandler
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<CommandResult<bool>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var address = new Address(command.Street, command.City, command.State, command.Country, command.ZipCode);
            var order = new Order(command.UserId, command.UserName, address, command.CardTypeId, command.CardNumber, command.CardSecurityNumber, command.CardHolderName, command.CardExpiration);
            foreach (var item in command.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            }

            await _orderRepository.CreateAsync(order, cancellationToken);
            return CommandResult<bool>.Success(true);
        }
    }
}
