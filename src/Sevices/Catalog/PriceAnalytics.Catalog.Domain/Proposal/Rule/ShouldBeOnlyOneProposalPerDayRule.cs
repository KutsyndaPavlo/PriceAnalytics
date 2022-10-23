using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;

namespace PriceAnalytics.Catalog.Domain.Proposal.Rule
{
    internal class ShouldBeOnlyOneProposalPerDayRule : IBusinessRule
    {
        private readonly IProposalService _proposalUniquenessChecker;
        private readonly ProposalEntity _proposal;

        public ShouldBeOnlyOneProposalPerDayRule(
            IProposalService proposalUniquenessChecker,
            ProposalEntity proposal)
        {
            _proposalUniquenessChecker = proposalUniquenessChecker;
            _proposal = proposal;
        }

        public bool IsBroken() => !_proposalUniquenessChecker.IsUnique(_proposal);

        public string Message => "Should be only one proposal per day.";
    }
}
