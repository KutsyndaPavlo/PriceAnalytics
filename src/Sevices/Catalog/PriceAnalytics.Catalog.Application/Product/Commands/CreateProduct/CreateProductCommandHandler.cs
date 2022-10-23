using AutoMapper;
using MediatR;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductById;
using PriceAnalytics.Catalog.Domain.Product;

namespace PriceAnalytics.Catalog.Application.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IProductService _productUniquenessChecker;

        public CreateProductCommandHandler(IApplicationDbContext applicationDbContext, 
                                           IMapper mapper, 
                                           IProductService productUniquenessChecker)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _productUniquenessChecker = productUniquenessChecker;
        }

        public async Task<ProductDto> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var entity = ProductEntity.CreateNew(
                command.Name,
                command.Code,
                command.Description,
                _productUniquenessChecker);

            _applicationDbContext.Products.Add(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProductDto>(entity);

            return dto;
        }
    }
}
