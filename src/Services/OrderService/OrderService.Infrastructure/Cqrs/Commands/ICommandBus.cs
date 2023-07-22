namespace OrderService.Infrastructure.Cqrs.Commands
{
    public interface ICommandBus
    {
        Task<CommandResult> SendAsync(ICommand command, CancellationToken cancellationToken = default);
        Task<CommandResult<TResponse>> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    }
}
