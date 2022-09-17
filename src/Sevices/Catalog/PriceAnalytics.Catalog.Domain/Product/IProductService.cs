using PriceAnalytics.Catalog.Domain.Entities;

namespace PriceAnalytics.Catalog.Domain.Product
{
    public interface IProductService
    {
        bool IsUniqueName(string name);

        IEnumerable<ProposalEntity> LatestProposals(ProductEntity product);

        IEnumerable<ProposalEntity> HistoricalProposals(ProductEntity product, int offset = 0, int limit = 1);
    }
}
