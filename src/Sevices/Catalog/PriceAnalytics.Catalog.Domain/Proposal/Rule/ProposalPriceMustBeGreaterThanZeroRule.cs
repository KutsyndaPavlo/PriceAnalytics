using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Domain.Proposal.Rule
{
    public class ProposalPriceMustBeGreaterThanZeroRule : IBusinessRule
    {
        private readonly float _price;

        public ProposalPriceMustBeGreaterThanZeroRule(float price)
        {
            _price = price;
        }

        public bool IsBroken() => _price <= 0;

        public string Message => "Price should be greater than zero.";
    }
}
