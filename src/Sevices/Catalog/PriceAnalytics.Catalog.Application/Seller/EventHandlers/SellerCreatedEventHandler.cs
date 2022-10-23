using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Catalog.Domain.Seller.Events;

namespace PriceAnalytics.Catalog.Application.Seller.EventHandlers
{
    internal class SellerCreatedEventHandler : INotificationHandler<DomainEventNotification<SellerCreatedEvent>>
    {
        private readonly ILogger<SellerCreatedEventHandler> _logger;

        public SellerCreatedEventHandler(ILogger<SellerCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<SellerCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
