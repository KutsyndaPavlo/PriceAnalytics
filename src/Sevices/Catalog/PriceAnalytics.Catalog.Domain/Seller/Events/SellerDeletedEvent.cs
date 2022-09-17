using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Domain.Seller.Events
{
    public class SellerDeletedEvent : DomainEvent
    {
        public SellerDeletedEvent(SellerEntity seller)
        {
            Seller = seller;
        }

        public SellerEntity Seller { get; }
    }
}