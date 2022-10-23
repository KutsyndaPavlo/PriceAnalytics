using MediatR;
using PriceAnalytics.Catalog.Application.Common.Model;
using System.Runtime.Serialization;

namespace PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById
{
    public class GetProposalByIdQuery : IRequest<ProposalDto>
    {
        public GetProposalByIdQuery(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }
    }
}
