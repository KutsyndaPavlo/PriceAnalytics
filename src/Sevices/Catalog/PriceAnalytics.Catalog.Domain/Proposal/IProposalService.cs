using PriceAnalytics.Catalog.Domain.Entities;

namespace PriceAnalytics.Catalog.Domain.Proposal
{
    public interface IProposalService
    {
        bool IsUnique(ProposalEntity proposal);
    }
}
