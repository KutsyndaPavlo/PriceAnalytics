using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductById;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Administration.Domain.Product.Specification;

namespace PriceAnalytics.Catalog.Application.Product.Commands.DeactivateProduct
{
    public class DeactivateProductCommandHandler : IRequestHandler<DeactivateProductCommand, ProductDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public DeactivateProductCommandHandler(IApplicationDbContext applicationDbContext, 
                                              IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(DeactivateProductCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Products.FirstOrDefaultAsync(new ProductByIdSpecification(command.Id).ToExpression());


            if (entity == null)
            {
                throw new ProductNotFoundException($"Product with id {command.Id} doesn't exist.");
            }

            if (command.LastModified != entity.LastModified)
            {
                throw new ProductConcurrentUpdateException($"Product {command.Id} version is outdated.");
            }

            entity.DeActivate();

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProductDto>(entity);

            return dto;
        }
    }
}
