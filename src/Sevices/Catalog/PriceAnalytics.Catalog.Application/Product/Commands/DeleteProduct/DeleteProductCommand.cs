using MediatR;

namespace PriceAnalytics.Catalog.Application.Product.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
