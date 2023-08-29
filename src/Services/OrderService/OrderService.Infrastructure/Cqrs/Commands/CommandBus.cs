using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.SeedWork;
using MediatR;

namespace OrderService.Infrastructure.Cqrs.Commands
{
    internal class CommandBus : ICommandBus
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;

        public CommandBus(IServiceProvider serviceProvider)
        {
            mediator = serviceProvider.GetRequiredService<IMediator>();
            unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public async Task<CommandResult> SendAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var commandResult = await unitOfWork.ExecuteAsync(() => mediator.Send(command, cancellationToken), cancellationToken);
                return commandResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    
            }
            return CommandResult.Error("");
        }

        public Task<CommandResult<TResponse>> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            return unitOfWork.ExecuteAsync(() => mediator.Send(command, cancellationToken), cancellationToken);
        }
    }
}
