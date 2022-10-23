using MediatR;
using PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById;
using System.Runtime.Serialization;

namespace PriceAnalytics.Catalog.Application.Product.Queries.GetProductProposals
{
    public class GetProductProposalsQuery : IRequest<IEnumerable<ProposalDto>>
    {
        public GetProductProposalsQuery(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }
    }
}
