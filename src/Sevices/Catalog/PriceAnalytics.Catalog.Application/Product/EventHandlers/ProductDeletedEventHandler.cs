using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Catalog.Domain.Product.Events;
using PriceAnalytics.Application.Common.Models;

namespace PriceAnalytics.Catalog.Application.Product.EventHandlers
{
    public class ProductDeletedEventHandler : INotificationHandler<DomainEventNotification<ProductDeletedEvent>>
    {
        private readonly ILogger<ProductDeletedEventHandler> _logger;

        public ProductDeletedEventHandler(ILogger<ProductDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ProductDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
