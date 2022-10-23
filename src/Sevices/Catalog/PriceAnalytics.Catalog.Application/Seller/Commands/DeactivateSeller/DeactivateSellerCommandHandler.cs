using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Seller.Commands.DeactivateSeller;
using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Seller.Spefifications;

namespace PriceAnalytics.Catalog.Application.Seller.Commands.CreateSeller
{
    public class DeactivateSellerCommandHandler : IRequestHandler<DeactivateSellerCommand, SellerDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public DeactivateSellerCommandHandler(IApplicationDbContext applicationDbContext, 
                                              IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<SellerDto> Handle(DeactivateSellerCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Sellers.FirstOrDefaultAsync(new SellerByIdSpecification(command.Id).ToExpression());

            if (entity == null)
            {
                throw new SellerNotFoundException($"Seller with id {command.Id} doesn't exist");
            }

            entity.DeActivate();

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<SellerDto>(entity);

            return dto;
        }
    }
}
