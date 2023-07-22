using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.SeedWork;
using OrderService.Infrastructure.Domain;

namespace OrderService.Infrastructure.Database
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly OrderServiceContext _context;
        private readonly IMediator _mediator;
        private bool isActiveTransaction;

        public UnitOfWork(OrderServiceContext context, IMediator mediator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            if (isActiveTransaction)
            {
                await action();
                return;
            }

            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                isActiveTransaction = true;
                try
                {
                    await action();

                    // Dispatch Domain Events collection. 
                    // Choices:
                    // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
                    // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
                    // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
                    // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers.
                    await _mediator.DispatchDomainEventsAsync(_context);

                    // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
                    // performed through the DbContext will be committed

                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
                finally
                {
                    isActiveTransaction = false;
                }
            });
        }

        public Task<TResponse> ExecuteAsync<TResponse>(Func<Task<TResponse>> action, CancellationToken cancellationToken = default)
        {
            if (isActiveTransaction) return action();

            var strategy = _context.Database.CreateExecutionStrategy();

            return strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                isActiveTransaction = true;
                try
                {
                    var result = await action();

                    // Dispatch Domain Events collection. 
                    // Choices:
                    // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
                    // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
                    // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
                    // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers.
                    await _mediator.DispatchDomainEventsAsync(_context);

                    // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
                    // performed through the DbContext will be committed

                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return result;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
                finally
                {
                    isActiveTransaction = false;
                }
            });
        }
    }
}
