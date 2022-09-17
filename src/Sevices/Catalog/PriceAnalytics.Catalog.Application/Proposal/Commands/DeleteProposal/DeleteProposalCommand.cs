using MediatR;

namespace PriceAnalytics.Catalog.Application.Proposal.Commands.DeleteProposal
{
    public class DeleteProposalCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
