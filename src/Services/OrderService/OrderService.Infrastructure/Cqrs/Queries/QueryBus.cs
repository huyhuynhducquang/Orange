﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace OrderService.Infrastructure.Cqrs.Queries
{
    internal class QueryBus : IQueryBus
    {
        private readonly IMediator _mediator;

        public QueryBus(IServiceProvider serviceProvider)
        {
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        public Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(query, cancellationToken);
        }
    }
}
