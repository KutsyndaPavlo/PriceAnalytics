using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;
using System.Linq.Expressions;

namespace PriceAnalytics.Administration.Domain.Proposal.Specification
{
    public class ProposalByIdSpecification : Specification<ProposalEntity>
    {
        private readonly Guid _id;

        public ProposalByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<ProposalEntity, bool>> ToExpression()
        {
            return (item) => (item.Id == _id);
        }
    }
}