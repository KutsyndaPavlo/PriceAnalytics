using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;

namespace PriceAnalytics.Catalog.Domain.Product.Events
{
    public class ProductDeletedEvent : DomainEvent
    {
        public ProductDeletedEvent(ProductEntity product)
        {
            Product = product;
        }

        public ProductEntity Product { get; }
    }
}