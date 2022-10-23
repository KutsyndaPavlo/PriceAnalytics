using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;
using System.Linq.Expressions;

namespace PriceAnalytics.Catalog.Domain.Product.Specification
{
    public class ActiveProductsOnDateSpecification : Specification<ProposalEntity>
    {
        private readonly DateTime _date;

        public ActiveProductsOnDateSpecification(DateTime date)
        {
            _date = date;
        }

        public override Expression<Func<ProposalEntity, bool>> ToExpression()
        {
            return (item) => (item.Date == _date && item.IsActive);
        }
    }
}