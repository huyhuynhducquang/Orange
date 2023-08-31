using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Models;
using OrderService.Application.Cqrs.Commands;
using OrderService.Infrastructure.Cqrs.Commands;

namespace OrderService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IEventBus eventBus;

        public OrdersController(ICommandBus commandBus, IEventBus eventBus)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus)); ;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
        {
            var command = new CreateOrderCommand(request.OrderItems.ToList(), request.UserId, request.UserName,
                request.City, request.Street, request.State, request.Country, request.ZipCode,
                request.CardNumber, request.CardHolderName, request.CardExpiration, request.CardSecurityNumber, request.CardTypeId);

            var result = await _commandBus.SendAsync(command);

            return Ok();
        }

        [HttpGet]
        public IActionResult TestPublisher()
        {
            eventBus.Publish(new OrderStartedIntegrationEvent(Guid.NewGuid().ToString()));
            return Ok();
        }
    }
}
