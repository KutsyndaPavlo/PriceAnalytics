using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Product;
using System.Linq.Expressions;

namespace PriceAnalytics.Administration.Domain.Product.Specification
{
    public class ProductByIdSpecification : Specification<ProductEntity>
    {
        private readonly Guid _id;

        public ProductByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<ProductEntity, bool>> ToExpression()
        {
            return (item) => (item.Id == _id);
        }
    }
}