using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;

namespace PriceAnalytics.Catalog.Domain.Proposal.Events
{
    public class ProposalCreatedEvent : DomainEvent
    {
        public ProposalCreatedEvent(ProposalEntity proposal)
        {
            Proposal = proposal;
        }

        public ProposalEntity Proposal { get; }
    }
}