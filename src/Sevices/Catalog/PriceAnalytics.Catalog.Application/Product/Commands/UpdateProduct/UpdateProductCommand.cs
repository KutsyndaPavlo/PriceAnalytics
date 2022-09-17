using MediatR;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductById;

namespace PriceAnalytics.Catalog.Application.Product.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public DateTime LastModified { get; set; }
    }
}
