using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Domain.Seller.Events
{
    public class SellerUpdatedEvent : DomainEvent
    {
        public SellerUpdatedEvent(SellerEntity seller)
        {
            Seller = seller;
        }

        public SellerEntity Seller { get; }
    }
}