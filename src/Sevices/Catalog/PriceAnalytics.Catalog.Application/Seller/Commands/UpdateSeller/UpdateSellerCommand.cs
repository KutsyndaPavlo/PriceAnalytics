using MediatR;
using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById;

namespace PriceAnalytics.Catalog.Application.Seller.Commands.UpdateSeller
{
    public class UpdateSellerCommand : IRequest<SellerDto>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime LastModified { get; set; }
    }
}
