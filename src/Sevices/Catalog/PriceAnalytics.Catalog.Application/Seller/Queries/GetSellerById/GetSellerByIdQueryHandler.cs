using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Seller.Spefifications;

namespace PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById
{
    public class GetSellerByIdQueryHandler : IRequestHandler<GetSellerByIdQuery, SellerDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetSellerByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<SellerDto> Handle(GetSellerByIdQuery query, CancellationToken cancellationToken)
        {
            var specification = new SellerByIdSpecification(query.Id);
            var entity = await _applicationDbContext.Sellers.FirstOrDefaultAsync(specification.ToExpression());

            if (entity == null)
            {
                throw new SellerNotFoundException($"Seller this id {query.Id} doesn't exist");
            }

            return _mapper.Map<SellerDto>(entity);
        }
    }
}
