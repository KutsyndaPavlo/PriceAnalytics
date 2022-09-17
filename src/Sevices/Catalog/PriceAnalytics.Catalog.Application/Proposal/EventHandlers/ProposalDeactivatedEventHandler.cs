using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Catalog.Domain.Proposal.Events;

namespace PriceAnalytics.Catalog.Application.Proposal.EventHandlers
{
    public class ProposalDeactivatedEventHandler : INotificationHandler<DomainEventNotification<ProposalDeactivatedEvent>>
    {
        private readonly ILogger<ProposalDeactivatedEventHandler> _logger;

        public ProposalDeactivatedEventHandler(ILogger<ProposalDeactivatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ProposalDeactivatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
