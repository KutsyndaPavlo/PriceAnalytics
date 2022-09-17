using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Domain.Product.Rules
{

    public class ProductNameMustBeUniqueRule : IBusinessRule
    {
        private readonly IProductService _productUniquenessChecker;

        private readonly string _name;

        public ProductNameMustBeUniqueRule(
            IProductService productUniquenessChecker,
            string name)
        {
            _productUniquenessChecker = productUniquenessChecker;
            _name = name;
        }

        public bool IsBroken() => !_productUniquenessChecker.IsUniqueName(_name);

        public string Message => "Product with this name already exists.";
    }
}
