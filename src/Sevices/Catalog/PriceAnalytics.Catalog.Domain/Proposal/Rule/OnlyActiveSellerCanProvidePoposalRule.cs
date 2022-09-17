using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Seller;

namespace PriceAnalytics.Catalog.Domain.Proposal.Rule
{
    public class OnlyActiveSellerCanProvidePoposalRule : IBusinessRule
    {
        private readonly SellerEntity _seller;

        public OnlyActiveSellerCanProvidePoposalRule(SellerEntity seller)
        {
            _seller = seller;
        }

        public bool IsBroken() => _seller == null || !_seller.IsActive;

        public string Message => "Only active seller can provide poposal.";
    }
}
