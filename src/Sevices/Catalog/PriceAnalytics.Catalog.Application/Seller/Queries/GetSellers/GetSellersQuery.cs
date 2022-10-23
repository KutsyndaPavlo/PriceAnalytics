using MediatR;
using PriceAnalytics.Catalog.Application.Common.Model;
using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById;
using System.Runtime.Serialization;

namespace PriceAnalytics.Catalog.Application.Seller.Queries.GetSellers
{
    public class GetSellersQuery : IRequest<PaginatedList<SellerDto>>
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Limit { get; set; }


        [DataMember]
        public int Offset { get; set; }

        public GetSellersQuery()
        {

        }

        public GetSellersQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
    }
}
