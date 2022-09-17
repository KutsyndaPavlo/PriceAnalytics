using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Domain.Seller.Events
{
    public class SellerCreatedEvent : DomainEvent
    {
        public SellerCreatedEvent(SellerEntity seller)
        {
            Seller = seller;
        }

        public SellerEntity Seller { get; }
    }
}