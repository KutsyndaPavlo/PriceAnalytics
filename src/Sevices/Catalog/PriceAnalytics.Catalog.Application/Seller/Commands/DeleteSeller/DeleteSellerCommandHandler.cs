using MediatR;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Seller;
using PriceAnalytics.Catalog.Domain.Seller.Events;

namespace PriceAnalytics.Catalog.Application.Seller.Commands.DeleteSeller
{
    public class DeleteSellerCommandHandler : IRequestHandler<DeleteSellerCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteSellerCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(DeleteSellerCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Sellers.FindAsync(new object[] { command.Id }, cancellationToken);

            if (entity == null)
            {
                throw new SellerNotFoundException($"Seller with id {command.Id} doesn't exist");
            }

            if (!entity.CanBeDeleted())
            {
                throw new SellerCanBeDeletedException($"Seller with id {command.Id} can't be deleted");
            }

            _applicationDbContext.Sellers.Remove(entity);

            entity.AddDomainEvent(new SellerDeletedEvent(entity));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
