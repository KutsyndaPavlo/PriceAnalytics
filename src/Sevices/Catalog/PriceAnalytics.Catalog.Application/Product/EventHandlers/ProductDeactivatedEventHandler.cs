using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Domain.Product.Events;
using PriceAnalytics.Catalog.Domain.Proposal.Specification;

namespace PriceAnalytics.Catalog.Application.Product.EventHandlers
{
    public class ProductDeactivatedEventHandler : INotificationHandler<DomainEventNotification<ProductDeactivatedEvent>>
    {
        private readonly ILogger<ProductDeactivatedEventHandler> _logger;
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductDeactivatedEventHandler(ILogger<ProductDeactivatedEventHandler> logger,
                                              IApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public async Task Handle(DomainEventNotification<ProductDeactivatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);
           
            await DeactivateProposals(domainEvent, cancellationToken);
        }

        private async Task DeactivateProposals(ProductDeactivatedEvent domainEvent, CancellationToken cancellationToken)
        {
            foreach (var item in _applicationDbContext.Proposals.Where(new ActiveProposalsForProductSpecification(domainEvent.Product.Id).ToExpression()))
            {
                item.DeActivate();
            }

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
