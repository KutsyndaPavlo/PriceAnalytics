using MediatR;

namespace PriceAnalytics.Catalog.Application.Seller.Commands.DeleteSeller
{
    public class DeleteSellerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
