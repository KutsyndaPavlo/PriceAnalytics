using MediatR;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductById;

namespace PriceAnalytics.Catalog.Application.Product.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public string Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }
    }
}
