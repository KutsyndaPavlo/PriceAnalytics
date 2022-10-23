using MediatR;
using PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById;

namespace PriceAnalytics.Catalog.Application.Product.Commands.CreateProduct
{
    public class DeactivateProposalCommand : IRequest<ProposalDto>
    {
        public Guid Id { get; set; }
    }
}
