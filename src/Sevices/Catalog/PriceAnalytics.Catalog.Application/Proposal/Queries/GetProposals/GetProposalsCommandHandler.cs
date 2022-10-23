using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Common.Model;
using PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById;
using PriceAnalytics.Catalog.Domain.Entities;

namespace PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposals
{
    public class GetProposalsCommandHandler
        : IRequestHandler<GetProposalsQuery, PaginatedList<ProposalDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetProposalsCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProposalDto>> Handle(GetProposalsQuery query, CancellationToken cancellationToken)
        {
            var root = (IQueryable<ProposalEntity>)_applicationDbContext.Proposals;

            var total = await root.LongCountAsync();

            var data = await root
                .OrderByDescending(c => c.Date)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync();

            return new PaginatedList<ProposalDto>
            {
                Items = data.Select(x => _mapper.Map<ProposalDto>(x)),
                Page = new Page
                {
                    Limit = query.Limit,
                    Offset = query.Offset,
                    Count = data.Count,
                    Total = total
                }
            };
        }
    }
}
