using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Catalog.Domain.Seller.Events;

namespace PriceAnalytics.Catalog.Application.Seller.EventHandlers
{
    internal class SellerUpdatedEventHandler : INotificationHandler<DomainEventNotification<SellerUpdatedEvent>>
    {
        private readonly ILogger<SellerUpdatedEventHandler> _logger;

        public SellerUpdatedEventHandler(ILogger<SellerUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<SellerUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
