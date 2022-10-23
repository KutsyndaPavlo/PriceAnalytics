using MediatR;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductById;

namespace PriceAnalytics.Catalog.Application.Product.Commands.DeactivateProduct
{
    public class DeactivateProductCommand : IRequest<ProductDto>
    {
        public Guid Id { get; set; }

        public DateTime LastModified { get; set; }
    }
}
