using MediatR;

namespace OrderService.Infrastructure.Cqrs.Commands
{
    public interface ICommand : IRequest<CommandResult>
    {
    }

    public interface ICommand<TResponse> : IRequest<CommandResult<TResponse>>
    {
    }
}
