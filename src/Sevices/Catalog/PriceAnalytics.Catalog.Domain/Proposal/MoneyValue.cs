using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Domain.Proposal
{
    public class MoneyValue : ValueObject
    {
        private MoneyValue() { }

        public float Price { get; }

        public string Currency { get; }

        public MoneyValue(float price, string currency)
        {
            Price = price;
            Currency = currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Price;
            yield return Currency;
        }
    }
}
