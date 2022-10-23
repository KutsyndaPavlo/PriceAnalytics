using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Proposal.Specification;
using PriceAnalytics.Administration.Domain.Proposal.Specification;

namespace PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById
{
    public class GetProposalByIdQueryHandler : IRequestHandler<GetProposalByIdQuery, ProposalDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetProposalByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ProposalDto> Handle(GetProposalByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Proposals.FirstOrDefaultAsync(new ProposalByIdSpecification(query.Id).ToExpression());

            if (entity == null)
            {
                throw new ProposalNotFoundException($"Proposal this id {query.Id} doesn't exist");
            }

            return _mapper.Map<ProposalDto>(entity);
        }
    }
}
