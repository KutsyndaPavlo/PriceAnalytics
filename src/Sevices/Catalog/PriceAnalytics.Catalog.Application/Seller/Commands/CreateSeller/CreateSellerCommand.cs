using MediatR;
using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById;

namespace PriceAnalytics.Catalog.Application.Product.Commands.CreateSeller
{
    public class CreateSellerCommand : IRequest<SellerDto>
    {
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
