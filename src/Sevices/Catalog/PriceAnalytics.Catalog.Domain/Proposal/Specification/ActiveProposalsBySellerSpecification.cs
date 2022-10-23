using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;
using System.Linq.Expressions;

namespace PriceAnalytics.Catalog.Domain.Proposal.Specification
{
    public class ActiveProposalsBySellerSpecification : Specification<ProposalEntity>
    {
        private readonly Guid _sellerId;

        public ActiveProposalsBySellerSpecification(Guid sellerId)
        {
            _sellerId = sellerId;
        }

        public override Expression<Func<ProposalEntity, bool>> ToExpression()
        {
            return (item) => (item.SellerId == _sellerId && item.IsActive);
        }
    }
}