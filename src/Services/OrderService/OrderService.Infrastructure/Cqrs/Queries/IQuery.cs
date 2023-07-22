using MediatR;

namespace OrderService.Infrastructure.Cqrs.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
