using PriceAnalytics.Catalog.Domain.Common;
using System.Linq.Expressions;

namespace PriceAnalytics.Catalog.Domain.Seller.Spefifications
{
    public class SellerByIdSpecification : Specification<SellerEntity>
    {
        private Guid _id;

        public SellerByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<SellerEntity, bool>> ToExpression()
        {
            return (item) => (item.Id == _id);
        }
    }
}
