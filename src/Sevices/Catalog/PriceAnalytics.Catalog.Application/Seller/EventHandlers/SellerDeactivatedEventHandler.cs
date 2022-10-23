using PriceAnalytics.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Domain.Proposal.Specification;
using PriceAnalytics.Catalog.Domain.Seller.Events;
using PriceAnalytics.Catalog.Domain.Seller.Spefifications;

namespace PriceAnalytics.Catalog.Application.Seller.EventHandlers
{
    public class SellerDeactivatedEventHandler : INotificationHandler<DomainEventNotification<SellerDeactivatedEvent>>
    {
        private readonly ILogger<SellerDeactivatedEventHandler> _logger;
        private readonly IApplicationDbContext _applicationDbContext;

        public SellerDeactivatedEventHandler(ILogger<SellerDeactivatedEventHandler> logger,
                                             IApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public async Task Handle(DomainEventNotification<SellerDeactivatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            await DeactivateProposals(domainEvent, cancellationToken);
        }
        private async Task DeactivateProposals(SellerDeactivatedEvent domainEvent, CancellationToken cancellationToken)
        {
            foreach (var item in _applicationDbContext.Proposals.Where(new ActiveProposalsBySellerSpecification(domainEvent.Seller.Id).ToExpression()))
            {
                item.DeActivate();
            }

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
