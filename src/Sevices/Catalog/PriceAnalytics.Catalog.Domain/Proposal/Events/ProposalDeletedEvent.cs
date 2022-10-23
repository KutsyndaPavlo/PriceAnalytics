using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;

namespace PriceAnalytics.Catalog.Domain.Proposal.Events
{
    public class ProposalDeletedEvent : DomainEvent
    {
        public ProposalDeletedEvent(ProposalEntity proposal)
        {
            Proposal = proposal;
        }

        public ProposalEntity Proposal { get; }
    }
}