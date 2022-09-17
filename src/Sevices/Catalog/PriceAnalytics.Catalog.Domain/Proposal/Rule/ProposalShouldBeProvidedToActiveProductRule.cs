using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Product;

namespace PriceAnalytics.Catalog.Domain.Proposal.Rule
{
    public class ProposalShouldBeProvidedToActiveProductRule : IBusinessRule
    {
        private readonly ProductEntity _product;

        public ProposalShouldBeProvidedToActiveProductRule(ProductEntity product)
        {
            _product = product;
        }

        public bool IsBroken() => _product == null || !_product.IsActive;

        public string Message => "Proposal should be provided to active productRule.";
    }
}
