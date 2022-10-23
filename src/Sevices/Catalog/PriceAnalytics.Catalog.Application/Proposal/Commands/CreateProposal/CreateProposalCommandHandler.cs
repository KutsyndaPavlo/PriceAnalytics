using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Entities;
using PriceAnalytics.Catalog.Domain.Proposal;
using PriceAnalytics.Catalog.Domain.Seller.Spefifications;
using PriceAnalytics.Administration.Domain.Product.Specification;

namespace PriceAnalytics.Catalog.Application.Proposal.Commands.CreateProposal
{
    public class CreateProposalCommandHandler : IRequestHandler<CreateProposalCommand, ProposalDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IProposalService _proposalUniquenessChecker;

        public CreateProposalCommandHandler(IApplicationDbContext applicationDbContext,
                                           IMapper mapper,
                                           IProposalService proposalUniquenessChecker)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _proposalUniquenessChecker = proposalUniquenessChecker;
        }

        public async Task<ProposalDto> Handle(CreateProposalCommand command, CancellationToken cancellationToken)
        {
            var seller = await _applicationDbContext.Sellers.FirstOrDefaultAsync(new SellerByIdSpecification(command.SellerId).ToExpression());

            if (seller == null)
            {
                throw new SellerNotFoundException($"Seller with id {command.SellerId} doesn't exist");
            }

            var product = await _applicationDbContext.Products.FirstOrDefaultAsync(new ProductByIdSpecification(command.ProductId).ToExpression());

            if (product == null)
            {
                throw new ProductNotFoundException($"Product with id {command.ProductId} doesn't exist");
            }

            var proposal = ProposalEntity.CreateNewAndActivate(
                product,
                seller,
                command.Price,
                command.Currency,
                command.Description,
                _proposalUniquenessChecker);

           _applicationDbContext.Proposals.Add(proposal);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProposalDto>(proposal);

            return dto;
        }
    }
}
