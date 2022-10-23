using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductById;
using PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Administration.Domain.Proposal.Specification;

namespace PriceAnalytics.Catalog.Application.Product.Commands.CreateProduct
{
    public class DeactivateProposalCommandHandler : IRequestHandler<DeactivateProposalCommand, ProposalDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public DeactivateProposalCommandHandler(IApplicationDbContext applicationDbContext, 
                                               IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ProposalDto> Handle(DeactivateProposalCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Proposals.FirstOrDefaultAsync(new ProposalByIdSpecification(command.Id).ToExpression());

            if (entity == null)
            {
                throw new ProposalNotFoundException($"Proposal with id {command.Id} doesn't exist");
            }

            entity.DeActivate();

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProposalDto>(entity);

            return dto;
        }
    }
}
