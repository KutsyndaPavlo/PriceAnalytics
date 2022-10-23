using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Catalog.Domain.Product.Events;
using PriceAnalytics.Administration.Domain.Product.Specification;

namespace PriceAnalytics.Catalog.Application.Product.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IApplicationDbContext applicationDbContext, IProductService productService)
        {
            _applicationDbContext = applicationDbContext;
            _productService = productService;
        }

        public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Products.FirstOrDefaultAsync(new ProductByIdSpecification(command.Id).ToExpression());

            if (entity == null)
            {
                throw new ProductNotFoundException($"Product with id {command.Id} doesn't exist");
            }

            if (!entity.CanBeDeleted(_productService))
            {
                throw new ProductCanNotBeDeletedException($"Product with id {command.Id} can't be deleted");
            }

            _applicationDbContext.Products.Remove(entity);

            entity.AddDomainEvent(new ProductDeletedEvent(entity));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
