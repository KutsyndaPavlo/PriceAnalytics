using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Catalog.Domain.Proposal.Events;

namespace PriceAnalytics.Catalog.Application.Proposal.EventHandlers
{
    public class ProposalDeletedEventHandler : INotificationHandler<DomainEventNotification<ProposalDeletedEvent>>
    {
        private readonly ILogger<ProposalDeletedEventHandler> _logger;

        public ProposalDeletedEventHandler(ILogger<ProposalDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ProposalDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
