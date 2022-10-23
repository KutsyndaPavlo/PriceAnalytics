using MediatR;
using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById;

namespace PriceAnalytics.Catalog.Application.Seller.Commands.DeactivateSeller
{
    public class DeactivateSellerCommand : IRequest<SellerDto>
    {
        public Guid Id { get; set; }

        public DateTime LastModified { get; set; }
    }
}
