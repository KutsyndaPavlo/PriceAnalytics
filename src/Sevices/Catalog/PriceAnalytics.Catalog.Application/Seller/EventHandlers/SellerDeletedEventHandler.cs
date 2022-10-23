using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Catalog.Domain.Seller.Events;

namespace PriceAnalytics.Catalog.Application.Seller.EventHandlers
{
    public class SellerDeletedEventHandler : INotificationHandler<DomainEventNotification<SellerDeletedEvent>>
    {
        private readonly ILogger<SellerDeletedEventHandler> _logger;

        public SellerDeletedEventHandler(ILogger<SellerDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<SellerDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
