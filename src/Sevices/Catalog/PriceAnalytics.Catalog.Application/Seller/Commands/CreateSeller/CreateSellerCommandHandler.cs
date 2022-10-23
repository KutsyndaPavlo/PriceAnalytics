using AutoMapper;
using MediatR;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Product.Commands.CreateSeller;
using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById;
using PriceAnalytics.Catalog.Domain.Seller;

namespace PriceAnalytics.Catalog.Application.Seller.Commands.CreateSeller
{
    public class CreateSellerCommandHandler : IRequestHandler<CreateSellerCommand, SellerDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CreateSellerCommandHandler(IApplicationDbContext applicationDbContext, 
                                           IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<SellerDto> Handle(CreateSellerCommand command, CancellationToken cancellationToken)
        {
            var entity = SellerEntity.CreateNewAndActivate(
                command.Name,
                command.Description);

            _applicationDbContext.Sellers.Add(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<SellerDto>(entity);

            return dto;
        }
    }
}
