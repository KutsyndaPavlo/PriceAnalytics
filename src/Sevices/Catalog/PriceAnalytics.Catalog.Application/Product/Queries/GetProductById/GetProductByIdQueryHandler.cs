using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Administration.Domain.Product.Specification;

namespace PriceAnalytics.Catalog.Application.Product.Queries.GetProductById
{
    public class GetProductByIdQueryHandler
        : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Products.FirstOrDefaultAsync(new ProductByIdSpecification(query.Id).ToExpression());

            if (entity == null)
            {
                throw new ProductNotFoundException($"Product with id {query.Id} doesn't exist");
            }

            return _mapper.Map<ProductDto>(entity);
        }
    }
}
