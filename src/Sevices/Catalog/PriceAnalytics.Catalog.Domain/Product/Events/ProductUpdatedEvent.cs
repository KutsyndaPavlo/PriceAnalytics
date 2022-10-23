using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Domain.Product.Events
{
    public class ProductUpdatedEvent : DomainEvent
    {
        public ProductUpdatedEvent(ProductEntity product)
        {
            Product = product;
        }

        public ProductEntity Product { get; }
    }
}