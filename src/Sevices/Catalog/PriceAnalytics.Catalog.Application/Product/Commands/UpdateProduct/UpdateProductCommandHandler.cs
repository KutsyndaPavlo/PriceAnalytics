using AutoMapper;
using MediatR;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductById;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Product;

namespace PriceAnalytics.Catalog.Application.Product.Commands.UpdateProduct
{
    internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IProductService _productUniquenessChecker;

        public UpdateProductCommandHandler(
            IApplicationDbContext applicationDbContext, 
            IMapper mapper,
            IProductService productUniquenessChecker)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _productUniquenessChecker = productUniquenessChecker;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Products.FindAsync(new object[] { command.Id }, cancellationToken);

            if (entity == null)
            {
                throw new ProductNotFoundException($"Product with id {command.Id} doesn't exist.");
            }

            if (command.LastModified != entity.LastModified)
            {
                throw new ProductConcurrentUpdateException($"Product {command.Id} version is outdated.");
            }            

            entity.UpdateProduct(
                command.Name,
                command.Code,
                command.Description,
                _productUniquenessChecker);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProductDto>(entity);

            return dto;
        }
    }
}
