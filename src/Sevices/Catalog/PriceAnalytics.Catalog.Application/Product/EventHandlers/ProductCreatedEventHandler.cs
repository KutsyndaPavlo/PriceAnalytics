using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Catalog.Domain.Product.Events;

namespace PriceAnalytics.Catalog.Application.Product.EventHandlers
{
    internal class ProductCreatedEventHandler : INotificationHandler<DomainEventNotification<ProductCreatedEvent>>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;

        public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ProductCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
