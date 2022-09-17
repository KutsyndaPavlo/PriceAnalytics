using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Catalog.Domain.Proposal.Events;
using PriceAnalytics.Catalog.Application.Proposal.IntegrationEvents;
using PriceAnalytics.Infrustructure.EventBus.Abstractions;

namespace PriceAnalytics.Catalog.Application.Proposal.EventHandlers
{
    internal class ProposalCreatedEventHandler : INotificationHandler<DomainEventNotification<ProposalCreatedEvent>>
    {
        private readonly ILogger<ProposalCreatedEventHandler> _logger;
        private readonly IEventBus _eventBus;

        public ProposalCreatedEventHandler(ILogger<ProposalCreatedEventHandler> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task Handle(DomainEventNotification<ProposalCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            var proposal = domainEvent.Proposal;  //ToDo send via queue

            var integrationEvent = new ProposalCreatedIntegrationEvent
            {
                Currency = proposal.Price.Currency,
                Price = proposal.Price.Price,
                Description = proposal.Description,
                ProductId = proposal.ProductId,
                SellerId = proposal.SellerId,
                //Id = proposal.Id,
            };

            _eventBus.Publish(integrationEvent);

            //await Task.Delay(TimeSpan.FromSeconds(1));
            domainEvent.IsPublished = true;

            _logger.LogInformation($"Domain Event: {domainEvent}", domainEvent.GetType().Name);
        }
    }
}
