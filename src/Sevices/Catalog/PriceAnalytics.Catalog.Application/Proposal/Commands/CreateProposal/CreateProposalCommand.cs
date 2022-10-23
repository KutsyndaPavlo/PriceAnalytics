using MediatR;
using PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById;

namespace PriceAnalytics.Catalog.Application.Proposal.Commands.CreateProposal
{
    public class CreateProposalCommand : IRequest<ProposalDto>
    {
        public Guid SellerId { get; set; }

        public Guid ProductId { get; set; }

        public float Price { get; set; }

        public string Currency { get; set; }

        public string? Description { get; set; }
    }
}
