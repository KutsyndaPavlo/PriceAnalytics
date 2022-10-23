using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Common.Model;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductById;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Catalog.Domain.Product.Spefifications;

namespace PriceAnalytics.Catalog.Application.Product.Queries.GetProducts
{
    public class GetProductsCommandHandler
        : IRequestHandler<GetProductsQuery, PaginatedList<ProductDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetProductsCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProductDto>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var root = (IQueryable<ProductEntity>)_applicationDbContext.Products;

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                root = root.Where(new ProductsNameEqualsSpecification(query.Name).ToExpression());
            }

            var total = await root.LongCountAsync();

            var data = await root
                .OrderBy(c => c.Name)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync();

            return new PaginatedList<ProductDto>
            {
                Items = data.Select(x => _mapper.Map<ProductDto>(x)),
                Page = new Page
                {
                    Limit = query.Limit,
                    Offset = query.Offset,
                    Count = data.Count,
                    Total = total
                }
            };
        }
    }
}
