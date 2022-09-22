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

            var integrationEvent = new ProposalCreatedIntegrationEvent
            {
                Currency = domainEvent.Proposal.Price.Currency,
                Price = domainEvent.Proposal.Price.Price,
                Description = domainEvent.Proposal.Description,
                ProductId = domainEvent.Product.Id,
                ProductName = domainEvent.Product.Name,
                ProductCode = domainEvent.Product.Code,
                SellerId = domainEvent.Seller.Id,
                SellerName = domainEvent.Seller.Name,
            };

            _eventBus.Publish(integrationEvent);

            //await Task.Delay(TimeSpan.FromSeconds(1));
            domainEvent.IsPublished = true;

            _logger.LogInformation($"Domain Event: {domainEvent}", domainEvent.GetType().Name);
        }
    }
}
