using MediatR;
using System.Runtime.Serialization;

namespace PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById
{
    public class GetSellerByIdQuery : IRequest<SellerDto>
    {
        public GetSellerByIdQuery(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }
    }
}
