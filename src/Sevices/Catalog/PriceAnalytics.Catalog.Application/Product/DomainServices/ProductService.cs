using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Domain.Entities;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Catalog.Domain.Product.Spefifications;
using PriceAnalytics.Catalog.Domain.Proposal.Specification;

namespace PriceAnalytics.Catalog.Application.Product.DomainServices
{
    public class ProductService : IProductService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<ProposalEntity> LatestProposals(ProductEntity product)
        {
            return _applicationDbContext.Proposals
                 .Where(new ActiveProposalsForProductSpecification(product.Id).ToExpression())
                 .Join(_applicationDbContext.Sellers,
                    pr => pr.SellerId,
                    sel => sel.Id,
                    (pr, sel) => new { pr, sel })
                 .Where(x => x.sel.IsActive)
                 .Select(x => x.pr)
                 .GroupBy(x => x.SellerId)
                 .Select(x => x.First(y => y.Date == x.Max(x => x.Date) && y.IsActive))
                 .ToList()
                 .Where(x => x != null);
        }

        public IEnumerable<ProposalEntity> HistoricalProposals(ProductEntity product, int offset = 0, int limit = 1)
        {
            var filterExpression = new ActiveProposalsForProductSpecification(product.Id).ToExpression();

            return _applicationDbContext.Proposals
                .Where(filterExpression)
                .OrderBy(x => x.Date)
                .Skip(offset)
                .Take(limit);
        }

        public bool IsUniqueName(string name)
        {
            return !_applicationDbContext.Products.Any(new ProductsNameEqualsSpecification(name).ToExpression());
        }
    }
}
