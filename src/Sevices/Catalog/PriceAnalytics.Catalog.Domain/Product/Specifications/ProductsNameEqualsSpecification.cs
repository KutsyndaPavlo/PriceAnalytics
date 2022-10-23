using PriceAnalytics.Catalog.Domain.Common;
using System.Linq.Expressions;

namespace PriceAnalytics.Catalog.Domain.Product.Spefifications
{
    public class ProductsNameEqualsSpecification : Specification<ProductEntity>
    {
        private readonly string _name;

        public ProductsNameEqualsSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<ProductEntity, bool>> ToExpression()
        {
            return (item) => (item.Name == _name);
        }
    }
}
