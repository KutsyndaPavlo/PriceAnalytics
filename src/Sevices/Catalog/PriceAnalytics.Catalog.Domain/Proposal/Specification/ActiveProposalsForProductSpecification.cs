using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;
using PriceAnalytics.Catalog.Domain.Product;
using System.Linq.Expressions;

namespace PriceAnalytics.Catalog.Domain.Proposal.Specification
{
    public class ActiveProposalsForProductSpecification : Specification<ProposalEntity>
    {
        private readonly Guid _productId;

        public ActiveProposalsForProductSpecification(Guid productId)
        {
            _productId = productId;
        }

        public override Expression<Func<ProposalEntity, bool>> ToExpression()
        {
            return (item) => (item.ProductId == _productId && item.IsActive);
        }
    }
}