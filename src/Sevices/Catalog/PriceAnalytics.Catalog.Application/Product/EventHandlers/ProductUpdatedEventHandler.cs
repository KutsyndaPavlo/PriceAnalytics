using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Catalog.Domain.Product.Events;

namespace PriceAnalytics.Catalog.Application.Product.EventHandlers
{
    internal class ProductUpdatedEventHandler : INotificationHandler<DomainEventNotification<ProductUpdatedEvent>>
    {
        private readonly ILogger<ProductUpdatedEventHandler> _logger;

        public ProductUpdatedEventHandler(ILogger<ProductUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ProductUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
