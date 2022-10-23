using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Domain.Product.Events
{
    public class ProductDeactivatedEvent : DomainEvent
    {
        public ProductDeactivatedEvent(ProductEntity product)
        {
            Product = product;
        }

        public ProductEntity Product { get; }
    }
}
