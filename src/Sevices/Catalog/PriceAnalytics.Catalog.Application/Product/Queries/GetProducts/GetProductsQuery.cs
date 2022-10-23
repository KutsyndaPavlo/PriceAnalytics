using MediatR;
using PriceAnalytics.Catalog.Application.Common.Model;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductById;
using System.Runtime.Serialization;

namespace PriceAnalytics.Catalog.Application.Product.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<PaginatedList<ProductDto>>
    {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Limit { get; set; }


        [DataMember]
        public int Offset { get; set; }

        public GetProductsQuery()
        {

        }

        public GetProductsQuery(string name, int offset, int limit)
        {
            Name = name;
            Offset = offset;
            Limit = limit;
        }
    }
}
