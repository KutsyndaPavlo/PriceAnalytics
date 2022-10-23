using AutoMapper;
using MediatR;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById;
using PriceAnalytics.Catalog.Domain;

namespace PriceAnalytics.Catalog.Application.Seller.Commands.UpdateSeller
{
    internal class UpdateSellerCommandHandler : IRequestHandler<UpdateSellerCommand, SellerDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public UpdateSellerCommandHandler(
            IApplicationDbContext applicationDbContext, 
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<SellerDto> Handle(UpdateSellerCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Sellers.FindAsync(new object[] { command.Id }, cancellationToken);

            if (entity == null)
            {
                throw new SellerNotFoundException($"Seller this id {command.Id} doesn't exist.");
            }

            if (command.LastModified != entity.LastModified)
            {
                throw new SellerConcurrentUpdateException($"Seller {command.Id} version is outdated.");
            }            

            entity.UpdateSeller(
                command.Name,
                command.Description);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<SellerDto>(entity);

            return dto;
        }
    }
}
