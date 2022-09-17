using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Domain.Entities;
using PriceAnalytics.Catalog.Domain.Product.Specification;
using PriceAnalytics.Catalog.Domain.Proposal;
using PriceAnalytics.Catalog.Domain.Proposal.Specification;

namespace PriceAnalytics.Catalog.Application.Proposal.DomainServices
{
    public class ProposalService : IProposalService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProposalService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool IsUnique(ProposalEntity proposal)
        {
            //return !_applicationDbContext.Proposals.Any(x =>
            //x.IsActive &&
            //x.ProductId == proposal.ProductId &&
            //x.SellerId == proposal.SellerId &&
            //x.Date == proposal.Date);

            var filterExpression = new ActiveProposalsForProductSpecification(proposal.ProductId)
                                    .And(new ActiveProposalsBySellerSpecification(proposal.SellerId))
                                    .And(new ActiveProductsOnDateSpecification(proposal.Date))
                                    .ToExpression();

            return !_applicationDbContext.Proposals.Any(filterExpression);            
        }
    }
}
