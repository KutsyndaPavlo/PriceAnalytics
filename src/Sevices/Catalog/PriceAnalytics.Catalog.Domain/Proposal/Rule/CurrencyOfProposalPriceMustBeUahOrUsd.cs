using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Domain.Proposal.Rule
{
    public class CurrencyOfProposalPriceMustBeUahOrUsd : IBusinessRule
    {
        private readonly string _currency;

        public CurrencyOfProposalPriceMustBeUahOrUsd(string currency)
        {
            _currency = currency;
        }

        public bool IsBroken() => !(_currency == "USD" || _currency == "UAH");

        public string Message => "Currency of proposal price must be Uah or Usd.";
    }
}
