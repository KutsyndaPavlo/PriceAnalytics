using MediatR;
using System.Runtime.Serialization;

namespace PriceAnalytics.Catalog.Application.Product.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }
    }
}
