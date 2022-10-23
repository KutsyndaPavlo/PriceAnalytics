using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Administration.Domain.Product.Specification;

namespace PriceAnalytics.Catalog.Application.Product.Queries.GetProductProposals
{
    public class GetProductProposalsQueryHandler
        : IRequestHandler<GetProductProposalsQuery, IEnumerable<ProposalDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public GetProductProposalsQueryHandler(
            IApplicationDbContext applicationDbContext,
            IMapper mapper,
            IProductService productService)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<IEnumerable<ProposalDto>> Handle(GetProductProposalsQuery query, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Products.FirstOrDefaultAsync(new ProductByIdSpecification(query.Id).ToExpression());

            if (entity == null)
            {
                throw new ProductNotFoundException($"Product this id {query.Id} doesn't exist");
            }

            var proposals = entity.ActiveProposals(_productService).ToList();

            return proposals.Select(x => _mapper.Map<ProposalDto>(x));
        }
    }
}
