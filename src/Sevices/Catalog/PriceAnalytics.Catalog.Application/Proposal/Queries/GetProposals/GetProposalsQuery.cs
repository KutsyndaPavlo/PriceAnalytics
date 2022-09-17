using MediatR;
using PriceAnalytics.Catalog.Application.Common.Model;
using PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById;
using System.Runtime.Serialization;

namespace PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposals
{
    public class GetProposalsQuery : IRequest<PaginatedList<ProposalDto>>
    {
        [DataMember]
        public int Limit { get; set; }


        [DataMember]
        public int Offset { get; set; }

        public GetProposalsQuery()
        {

        }

        public GetProposalsQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
    }
}
