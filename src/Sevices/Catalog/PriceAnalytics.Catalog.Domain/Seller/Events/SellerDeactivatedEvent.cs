using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Domain.Seller.Events
{
    public class SellerDeactivatedEvent : DomainEvent
    {
        public SellerDeactivatedEvent(SellerEntity seller)
        {
            Seller = seller;
        }

        public SellerEntity Seller { get; }
    }
}
