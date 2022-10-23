using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Common.Model;
using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById;
using PriceAnalytics.Catalog.Domain.Seller;

namespace PriceAnalytics.Catalog.Application.Seller.Queries.GetSellers
{
    public class GetSellersCommandHandler
        : IRequestHandler<GetSellersQuery, PaginatedList<SellerDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetSellersCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<SellerDto>> Handle(GetSellersQuery query, CancellationToken cancellationToken)
        {
            var root = (IQueryable<SellerEntity>)_applicationDbContext.Sellers;

            var total = await root.LongCountAsync();

            var data = await root
                .OrderBy(c => c.Name)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync();

            return new PaginatedList<SellerDto>
            {
                Items = data.Select(x => _mapper.Map<SellerDto>(x)),
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
