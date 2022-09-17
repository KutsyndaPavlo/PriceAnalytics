using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;

namespace PriceAnalytics.Catalog.Domain.Product.Events
{
    public class ProductCreatedEvent : DomainEvent
    {
        public ProductCreatedEvent(ProductEntity product)
        {
            Product = product;
        }

        public ProductEntity Product { get; }
    }
}