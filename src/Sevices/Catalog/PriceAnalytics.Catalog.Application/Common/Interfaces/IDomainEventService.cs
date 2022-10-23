using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
